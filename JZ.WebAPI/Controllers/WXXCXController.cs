using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Contract.Dtos.Input;
using JZ.Application.Contract.Dtos.WXAPP;
using JZ.Application.Infrastructure;
using JZ.Application.WXAPP;
using JZ.WebAPI.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using static SKIT.FlurlHttpClient.Wechat.TenpayV3.Models.CreateNewTaxControlFapiaoApplicationRequest.Types.Fapiao.Types;
using static SKIT.FlurlHttpClient.Wechat.TenpayV3.Models.DepositMarketingMemberCardOpenCardCodesResponse.Types;

namespace JZ.WebAPI.Controllers
{

    /// <summary>
    /// 微信小程序后台接口 
    /// </summary>
    public class WXXCXController : BaseController
    {
        private readonly ILogger<WXXCXController> logger;
        private readonly IConfiguration configuration;
        private readonly BannerService bannerService;
        private readonly WachatMicroAppService wachatMicroAppService;
        private readonly ZDXXService zDXXService;
        private readonly FWXXService fWXXService;
        private readonly DTGLService dTGLService;
        private readonly OrderService orderService;
        private readonly LXWMService lXWMService;
        private readonly AddressService addressService;
        private readonly AssessService assessService;
        private readonly UserInfoService userInfoService;
        public WXXCXController(ILogger<WXXCXController> logger, IConfiguration configuration
            , BannerService bannerService, WachatMicroAppService wachatMicroAppService
            ,ZDXXService zDXXService,FWXXService fWXXService,DTGLService dTGLService,OrderService orderService
            ,LXWMService lXWMService,AddressService addressService,AssessService assessService,UserInfoService userInfoService)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.bannerService = bannerService;
            this.wachatMicroAppService = wachatMicroAppService;
            this.zDXXService = zDXXService;
            this.fWXXService = fWXXService;
            this.dTGLService = dTGLService;
            this.orderService = orderService;
            this.lXWMService = lXWMService;
            this.addressService = addressService;
            this.assessService = assessService;
            this.userInfoService = userInfoService;
        }


        /// <summary>
        /// 获取微信session_key
        /// </summary>
        /// <param name="jscode"></param>
        /// <returns></returns>
        [HttpGet("LoginGetSessionCode")]
        public async Task<ApiResponse> LoginGetSessionCode(string jscode)
        {
            logger.LogInformation($"获取sessionkey,jscode{jscode}");
            var model = await wachatMicroAppService.GetLoginSession(jscode);
            return ApiResponse.Success(model);
        }

        /// <summary>
        /// 获取微信电话号码
        /// </summary>
        /// <param name="code">手机号获取凭证</param>
        /// <param name="openid">用于更新电话号码</param>
        /// <returns></returns>
        [HttpGet("GetPhoneNumber")]
        public async Task<ApiResponse> GetPhoneNumber(string code,string openid)
        {
            logger.LogInformation($"获取GetPhoneNumber,code:{code}");
            var model = await wachatMicroAppService.GetPhoneNumber(code, openid);
            return ApiResponse.Success(model);
        }


        /// <summary>
        /// 获取Banner图
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBanner")]
        public async Task<ApiResponse> GetBanner()
        {
            logger.LogInformation("进入控制器");
            var dtos=await bannerService.GetBannerListWithCache();
            if (dtos!=null)
            {
                foreach (var item in dtos)
                {
                    item.Addr = item.Addr.Replace("\\", "/");
                }
            }
            
            return ApiResponse.Success(dtos);
        }

        /// <summary>
        /// 获取首页的优质服务
        /// </summary>
        /// <returns></returns>
        [HttpGet("TopService")]
        public async Task<ApiResponse> GetTopCateGory()
        {
            var dtos = await fWXXService.GetTopService();
            int count = int.Parse(configuration["WXAPPCONFIG:topServiceCount"] ?? "6");
            if (dtos != null)
            {
                dtos = dtos.OrderBy(n => n.SX).Take(count).ToList();
                //将图文中的图片加上链接
                foreach (var item in dtos)
                {
                    item.FWLXMC = await zDXXService.GetZDMC("000003", item.FWLX);
                    item.UnitMC = await zDXXService.GetZDMC("000004", item.Unit);
                }
            }
            
            
            return ApiResponse.Success(dtos);
        }
        /// <summary>
        /// 根据服务id获取服务详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ServiceDetails")]
        public async Task<ApiResponse> GetServiceDetails(long id)
        {
            var model = await fWXXService.GetModel(id);
            if (model!=null)
            {
                model.Context = model.Context.Replace("/upload", configuration["MangeUrl"] + "/upload").Replace("\\", "/");
                model.FWLXMC = await zDXXService.GetZDMC("000003", model.FWLX);
                model.UnitMC = await zDXXService.GetZDMC("000004", model.Unit);
                model.Pic = model.Pic.Replace("\\", "/");
                model.Banner=model.Banner.Replace("\\", "/");
            }
            return ApiResponse.Success(model);
                 
        }

        /// <summary>
        /// 获取所有的品类
        /// </summary>
        /// <returns></returns>
        [HttpGet("CateGory")]
        public async Task<ApiResponse> GetAllCateGory()
        {
            var dtos = await zDXXService.GetZDXXListWithCache();
            if (dtos != null)
            {
                dtos = dtos.Where(n => n.ZDFLBM == "000003").OrderBy(n => n.ZDSX).ToList();
            }
            return ApiResponse.Success(dtos);
        }
        /// <summary>
        /// 根据分类编码获取服务详情
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("Service")]
        public async Task<ApiResponse> GetServiceByCate(string code)
        {
            var dtos= await fWXXService.GetListByLXBM(code);
            if (dtos!=null)
            {
                dtos = dtos.OrderBy(n => n.SX).ToList();
                foreach (var item in dtos)
                {
                    item.FWLXMC = await zDXXService.GetZDMC("000003", item.FWLX);
                    item.UnitMC = await zDXXService.GetZDMC("000004", item.Unit);
                    item.Pic = item.Pic.Replace("\\", "/");
                   
                }
            }
            return ApiResponse.Success(dtos);
        }

        /// <summary>
        /// 获取动态新闻列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("DTXWList")]
        public async Task<ApiResponse> GetDTXW()
        {
            var list = await dTGLService.GetList();
            if (list!=null)
            {
                foreach (var item in list)
                {
                    
                    item.Pic= item.Pic.Replace("\\", "/");
                }
            }
            return ApiResponse.Success(list);
        }
        /// <summary>
        /// 获取动态新闻详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DTXWDetails")]
        public async Task<ApiResponse> DTXWDetails(long id)
        {
            var model = await dTGLService.GetModel(id);
            if (model!=null)
            {
                model.Pic = model.Pic.Replace("\\", "/");
                model.AddTime=Utils.FormatDateTime(model.AddTime,"yyyy-MM-dd");
                model.Content = model.Content.Replace("/upload", configuration["MangeUrl"] + "/upload").Replace("\\", "/");
            }
            return ApiResponse.Success(model);
        }
        /// <summary>
        /// 获取关于我们
        /// </summary>
        /// <returns></returns>
        [HttpGet("LXWM")]
        public async Task<ApiResponse> GetLXWM()
        {
            var Model = await lXWMService.GetModel();
            return ApiResponse.Success(Model);
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddressList")]
        public async Task<ApiResponse> GetAddressList(string openid)
        {
            var list= await addressService.Getlist(openid);
            return ApiResponse.Success(list);
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet("OrderList")]
        public async Task<ApiResponse> GetOrderList(string openid)
        {
            var list = await orderService.GetOrderListByOpenID(openid);
            if (list!=null)
            {
                foreach (var item in list)
                {
                    item.OrderTime = Utils.FormatDateTime(item.OrderTime);
                }
            }
            return ApiResponse.Success(list);
        }
        /// <summary>
        /// 获取优质评价
        /// </summary>
        /// <returns></returns>
        [HttpGet("YZPJ")]
        public async Task<ApiResponse> GetYZPJ()
        {
            var list = await assessService.GetTopAssess();
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.AddTime= Utils.FormatDateTime(item.AddTime);
                }
            }
            return ApiResponse.Success(list);
        }
        /// <summary>
        /// 根据订单号获取评价
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpGet("GetPJ")]
        public async Task<ApiResponse> GetPJ(long orderid)
        {
            var Model= await assessService.GetModelByOrderid(orderid);
            if (Model != null)
            {
                Model.AddTime = Utils.FormatDateTime(Model.AddTime);
            }
            return ApiResponse.Success(Model);
        }

        /// <summary>
        /// 根据服务获取评价列表
        /// </summary>
        /// <param name="serviceid"></param>
        /// <returns></returns>
        [HttpGet("GetPJByServcie")]
        public async Task<ApiResponse> GetPJByServcie(long serviceid)
        {
            var list = await assessService.GetListByService(serviceid);
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.AddTime = Utils.FormatDateTime(item.AddTime);
                }
            }
            return ApiResponse.Success(list);
        }

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet("Profile")]
        public async Task<ApiResponse> GetProfile(string openid)
        {
            var model=await userInfoService.GetModelByOpenID(openid);
            return ApiResponse.Success(model);
        }
        
        /// <summary>
        /// 保存地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SaveAddress")]
        public async Task<ApiResponse> SaveAddress([FromBody] WXAddressInput input)
        {
            if (!string.IsNullOrWhiteSpace(input.openID))
            {
                var userModel = await userInfoService.GetModelByOpenID(input.openID);
                input.UserID = userModel.ID;
            }

            bool result= await addressService.WXSave(input);
            if (result)
            {
                return ApiResponse.Success("保存成功");
            }
            else
            {
                return ApiResponse.Fail("保存失败");
            }
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("DelAddress")]
        public async Task<ApiResponse>DelAddress(long id)
        {
            bool result = await addressService.Delete(new long[] { id });
            if (result)
            {
                return ApiResponse.Success("删除成功");
            }
            else
            {
                return ApiResponse.Fail("删除失败");
            }
        }


        /// <summary>
        /// 支付成功后更新订单状态
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        [HttpPost("PaySuccess")]
        public async Task<ApiResponse> PaySuccess(long orderid)
        {
            bool result= await orderService.UpdateOrderState(orderid, "1");
            if (result)
            {
                return ApiResponse.Success("更新成功");
            }
            else
            {
                logger.LogWarning($"小程序更新订单状态出错，订单id:{orderid}");
                return ApiResponse.Fail("更新失败");
            }
        }

        /// <summary>
        /// 小程序上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost("Uploadimg")]
        public async Task<ApiResponse> UpLoadImg()
        {
            var savepath = configuration["UserAssessPicPath"];
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }
            var formFile = HttpContext.Request.Form.Files[0];
            string ext = Path.GetExtension(formFile.FileName);
            string newFileName = string.Concat(Utils.GetDateMillsecondStr(),Utils.GetOrderCode(), ext);
            var fullfulename = Path.Combine(savepath, newFileName);
            using (var stream = System.IO.File.Create(fullfulename))
            {
                await formFile.CopyToAsync(stream);
            }
            return ApiResponse.Success("/upload/yhpj/" + newFileName);
        }

        /// <summary>
        /// 上传评价
        /// </summary>
        /// <returns></returns>
        [HttpPost("PJ")]
        public async Task<ApiResponse> SavePJ([FromBody] AssessInput input)
        {

            bool exist = await assessService.Exists(input.OrderID);
            if (exist)
            {
                return ApiResponse.Fail("当前订单已评价！");
            }

            bool result= await assessService.WXSavePJ(input);
            if (result)
            {
                return ApiResponse.Success("评价成功");
            }
            else
            {
                return ApiResponse.Fail("评价失败，请稍后重试！");
            }
        }

        /// <summary>
        /// 用户保存头像
        /// </summary>
        /// <returns></returns>
        [HttpPost("SaveUserPhoto")]
        public async Task<ApiResponse> SaveUserPhoto([FromBody] WXSavePhotoInput input)
        {
            if (string.IsNullOrWhiteSpace(input.PhotoUrl)||string.IsNullOrWhiteSpace(input.openID))
            {
                return ApiResponse.Fail("缺少参数!");
            }
            bool result= await userInfoService.SaveUserPhoto(input);
            if (result)
            {
                return ApiResponse.Success("保存成功！");
            }
            else
            {
                return ApiResponse.Fail("保存失败！");
            }

        }
        /// <summary>
        /// 保存用户名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SaveUserName")]
        public async Task<ApiResponse> SaveUserName([FromBody] WXSaveUserNameInput input)
        {
            if (string.IsNullOrWhiteSpace(input.UserName) || string.IsNullOrWhiteSpace(input.openID))
            {
                return ApiResponse.Fail("缺少参数!");
            }
            bool result = await userInfoService.SaveUserName(input);
            if (result)
            {
                return ApiResponse.Success("保存成功！");
            }
            else
            {
                return ApiResponse.Fail("保存失败！");
            }

        }

    }
}
