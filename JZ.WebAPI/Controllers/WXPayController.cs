using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Infrastructure;
using JZ.Application.Infrastructure;
using JZ.WebAPI.Common;
using JZ.WebAPI.Options;
using JZ.WebAPI.Services.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SKIT.FlurlHttpClient.Wechat.TenpayV3;
using SKIT.FlurlHttpClient.Wechat.TenpayV3.Models;
using System.Text;
using static SKIT.FlurlHttpClient.Wechat.TenpayV3.Models.QueryMarketingPayGiftActivityMerchantsResponse.Types;

namespace JZ.WebAPI.Controllers
{
    /// <summary>
    /// 微信支付相关控制器
    /// </summary>
    public class WXPayController : BaseController
    {
        private readonly ILogger<WXPayController> logger;
        private readonly Options.TenpayOptions _tenpayOptions;
        private readonly IWechatTenpayHttpClientFactory _tenpayHttpClientFactory;
        private readonly IConfiguration configuration;
        private readonly OrderService orderService;
        private readonly FWXXService fWXXService;
        private readonly UserInfoService userInfoService;
        private readonly ZDXXService zDXXService;
        private readonly AddressService addressService;
        private readonly IMailNotifyService mailNotifyService;

        public WXPayController(ILogger<WXPayController> logger,
            IOptions<Options.TenpayOptions> tenpayOptions,
            Services.HttpClients.IWechatTenpayHttpClientFactory tenpayHttpClientFactory,
            IConfiguration configuration,OrderService orderService,FWXXService fWXXService,UserInfoService userInfoService
            ,ZDXXService zDXXService,AddressService addressService, IMailNotifyService mailNotifyService)
        {
            this.logger = logger;
            this._tenpayOptions = tenpayOptions.Value;
            this._tenpayHttpClientFactory = tenpayHttpClientFactory;
            this.configuration = configuration;
            this.orderService = orderService;
            this.fWXXService = fWXXService;
            this.userInfoService = userInfoService;
            this.zDXXService = zDXXService;
            this.addressService = addressService;
            this.mailNotifyService = mailNotifyService;
        }
        /// <summary>
        /// 小程序下单接口
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("Jsapi")]
        public async Task<ApiResponse> CreateOrderByJsapi([FromBody] Models.CreateOrderByJsapiRequest requestModel)
        {
            long a = Utils.GetSnowID();//内部订单号
            logger.LogInformation($"小程序端调用Jsapi接口,FWID：{requestModel.FWID},openid:{requestModel.OpenId}");
            logger.LogInformation($"小程序下单接口接收数据,{JsonConvert.SerializeObject(requestModel)}");
            mailNotifyService.SendOrderNotify(requestModel.OpenId);
            if (requestModel.FWID<=0||string.IsNullOrWhiteSpace(requestModel.OpenId))
            {
                return ApiResponse.Fail("缺少参数，请稍后重试！");
            }
            var fwModel = await fWXXService.GetModel(requestModel.FWID);
            if (fwModel == null)
            {
                return ApiResponse.Fail("未查询到服务信息，请稍后重试！");
            }
            fwModel.FWLXMC = await zDXXService.GetZDMC("000003", fwModel.FWLX);
            var userModel = await userInfoService.GetModelByOpenID(requestModel.OpenId);
            if (userModel==null)
            {
                return ApiResponse.Fail("未查询到用户信息，请稍后重试！");
            }
            var client = _tenpayHttpClientFactory.Create(_tenpayOptions.Merchants[0].MerchantId);
            long Orderid = Utils.GetSnowID();//内部订单号
            int fwAmount = Convert.ToInt32(fwModel.Price * requestModel.FWSL * 100);//订单金额
            string description = $"郑小马帮帮-服务项目：{fwModel.Title}-{requestModel.FWSL}个";//订单描述信息
            var request = new CreatePayTransactionJsapiRequest()
            {
                OutTradeNumber = Orderid.ToString(),
                AppId = configuration["WXConf:appid"] ?? "",
                Description = description,
                NotifyUrl = _tenpayOptions.NotifyUrl,
                Amount = new CreatePayTransactionJsapiRequest.Types.Amount() { Total = fwAmount },
                Payer = new CreatePayTransactionJsapiRequest.Types.Payer() { OpenId = requestModel.OpenId }
            };
            var response = await client.ExecuteCreatePayTransactionJsapiAsync(request, cancellationToken: HttpContext.RequestAborted);
            if (!response.IsSuccessful())
            {
                logger.LogWarning($"JSAPI 下单失败（状态码：{response.RawStatus}，错误代码：{response.ErrorCode}，错误描述：{response.ErrorMessage}）。");
            }
            else
            {
                //创建内部订单
                bool insertFlag = await orderService.WXOrder(new Application.Contract.Dtos.Business.OrderDto()
                {
                    ID=Orderid,
                    WXPayOrderID ="",
                    UserID= userModel.ID,
                    FWID =requestModel.FWID,
                    Address=requestModel.LXDZ,
                    OrderPhone=requestModel.LXDH,
                    OrderTime=requestModel.SMSJ.PadRight(14,'0'),
                    OrderState="0",
                    Del=0,
                    Code=Utils.GetOrderCode(),
                    FWSL=requestModel.FWSL,
                    OrderUserName=requestModel.LXR,
                    Remarks=requestModel.BZ,
                    Title=$"{fwModel.FWLXMC}-{fwModel.Title}",
                    WXID= requestModel.OpenId,
                    Price=fwModel.Price,
                    Amount= fwModel.Price * requestModel.FWSL,
                    WXPrepayId=response.PrepayId,
                });
                //将订单的地址更新到用户的地址列表中
                await addressService.SaveOrderAddr(new Application.Contract.Dtos.Input.WXAddressInput() { 
                    Address = requestModel.LXDZ,
                    DefaultFlag="0",
                    openID= requestModel.OpenId,
                    UserID= userModel.ID,
                    Phone= requestModel.LXDH
                });

                if (!insertFlag)
                {
                    logger.LogWarning($"微信 JSAPI下单成功，内部订单创建失败，FWID：{requestModel.FWID},openid:{requestModel.OpenId}.用户电话：{userModel.Phone}");
                }
            }

            return ApiResponse.Success(response, "下单成功");
        }

        /// <summary>
        /// 小程序通知接口
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="signature"></param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        [HttpPost("Notify")]
        public async Task<ApiResponse> ReceiveMessage(
            [FromHeader(Name = "Wechatpay-Timestamp")] string timestamp,
            [FromHeader(Name = "Wechatpay-Nonce")] string nonce,
            [FromHeader(Name = "Wechatpay-Signature")] string signature,
            [FromHeader(Name = "Wechatpay-Serial")] string serialNumber)
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            string content = await reader.ReadToEndAsync();
            logger.LogInformation($"接收到微信支付推送的数据：{content};echatpay-Timestamp:{timestamp};" +
                $"Wechatpay-Nonce:{nonce};Wechatpay-Signature:{signature};Wechatpay-Serial:{serialNumber};");
            
            string merchantId = _tenpayOptions.Merchants[0].MerchantId;
            var client = _tenpayHttpClientFactory.Create(merchantId);
            bool valid = client.VerifyEventSignature(
                callbackTimestamp: timestamp,
                callbackNonce: nonce,
                callbackBody: content,
                callbackSignature: signature,
                callbackSerialNumber: serialNumber
            );
            if (!valid)
            {
                // NOTICE:
                //   需提前注入 CertificateManager、并下载平台证书，才可以使用扩展方法执行验签操作。
                //   请参考本示例项目 TenpayCertificateRefreshingBackgroundService 后台任务中的相关实现。
                //   有关 CertificateManager 的完整介绍请参阅《开发文档 / 基础用法 / 如何验证回调通知事件签名？》。
                //   后续如何解密并反序列化，请参阅《开发文档 / 基础用法 / 如何解密回调通知事件中的敏感数据？》。
                logger.LogInformation($"解析微信支付后台通知数据出错，错误数据：{content};echatpay-Timestamp:{timestamp};" +
               $"Wechatpay-Nonce:{nonce};Wechatpay-Signature:{signature};Wechatpay-Serial:{serialNumber};");
                return ApiResponse.Fail("通知失败");
            }
            var callbackModel = client.DeserializeEvent(content);
            var eventType = callbackModel.EventType?.ToUpper();
            
            switch (eventType)
            {
                case "TRANSACTION.SUCCESS":
                    {
                        var callbackResource = client.DecryptEventResource<SKIT.FlurlHttpClient.Wechat.TenpayV3.Events.TransactionResource>(callbackModel);
                        logger.LogInformation($"接收到微信支付推送的订单支付成功通知，商户订单号：{callbackResource.OutTradeNumber},微信支付订单号：{callbackResource.TransactionId}");
                        // 根据订单号更新订单状态
                        await orderService.WXPayNotice(callbackResource.OutTradeNumber, callbackResource.TransactionId);
                    }
                    break;

                default:
                    {
                        // 其他情况略
                        logger.LogInformation($"接收到微信支付推送的订单支付成功通知，交易编码：{eventType}");
                    }
                    break;
            }

            return ApiResponse.Success("通知成功");
        }

    }
}
