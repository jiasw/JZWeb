using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Dtos.WXAPP;
using JZ.Application.Contract.Infrastructure;
using JZ.Application.Infrastructure;
using JZ.Domain.Repository;
using JZ.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JZ.Application.WXAPP
{
    /// <summary>
    /// 微信小程序服务接口
    /// </summary>
    public class WachatMicroAppService
    {
        private readonly ICacheService cacheService;
        private readonly ILogger<WachatMicroAppService> logger;
        private readonly IConfiguration configuration;
        private readonly UserInfoRepository userInfoRepository;
        private readonly HttpClient client = new HttpClient();
        public WachatMicroAppService(ICacheService cacheService,ILogger<WachatMicroAppService> logger
            ,IConfiguration configuration, UserInfoRepository userInfoRepository)
        {
            this.cacheService = cacheService;
            this.logger = logger;
            this.configuration = configuration;
            this.userInfoRepository = userInfoRepository;
        }

        /// <summary>
        /// 微信小程序用户登录
        /// </summary>
        /// <param name="jscode"></param>
        /// <returns></returns>
        public async Task<WXAPPLoginDto> GetLoginSession(string jscode)
        {
            WXAPPLoginDto dto = new WXAPPLoginDto();
            logger.LogInformation($"调用GetLoginSession服务，参数：jscode {jscode}");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["appid"] = configuration["WXConf:appid"];
            query["secret"] = configuration["WXConf:secret"];
            query["js_code"] = jscode;
            query["grant_type"] = "authorization_code";
            string url = configuration["WXURL:code2Session"] + "?" + query.ToString();
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            WXLoginResponse wxresponse = JsonConvert.DeserializeObject<WXLoginResponse>(responseBody);
            logger.LogInformation($"调用GetLoginSession接口,微信官方返回： {responseBody}");
            if (wxresponse.errcode == 0)
            {
                //将微信的用户信息保存到数据库中
                dto=  new WXAPPLoginDto() { openID = wxresponse.openid, sessionKey = wxresponse.session_key };
                bool existopenid= await userInfoRepository.AsQueryable().Where(n=>n.WXID==wxresponse.openid).AnyAsync();
                if (!existopenid)
                {
                    //创建用户
                    long userid= await userInfoRepository.InsertReturnSnowflakeIdAsync(new Domain.Entitys.JZ_YW_USERINFO() { 
                    WXID=wxresponse.openid,
                    SessionKey=wxresponse.session_key,
                    AddTime=Utils.GetDateStr(),
                    Del=0,
                    NickName="郑好帮",
                    Phone="",
                    Photo="/img/defaultPhoto.png",
                    RealName="郑好帮",
                    Sex="1",
                    YHLY="1",
                    });
                }

            }
            return dto;
        }

        /// <summary>
        /// 获取电话号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<WXGetPhoneDto> GetPhoneNumber( string code,string openid)
        {
            string accessToken = await GetAccessTokenByCache();
            logger.LogInformation($"调用GetPhoneNumber,code:{code},accesstoken:{accessToken}");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["access_token"] = accessToken;
            string url = configuration["WXURL:getPhoneNumber"] + "?" + query.ToString();

            string content = $"{{\"code\":\"{code}\"}}";
            byte[] data = Encoding.UTF8.GetBytes(content);
            var bytearray = new ByteArrayContent(data);

            using HttpResponseMessage response = await client.PostAsync(url, bytearray);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            logger.LogInformation($"调用GetPhoneNumber接口,微信官方返回： {responseBody}");
            WXGetPhoneDto wXGetPhoneDto= JsonConvert.DeserializeObject<WXGetPhoneDto>(responseBody);
            if (wXGetPhoneDto!=null&&wXGetPhoneDto.errcode==0)
            {
                //更新用户表的电话号码
                await userInfoRepository.AsUpdateable(new Domain.Entitys.JZ_YW_USERINFO() { Phone=wXGetPhoneDto.phone_info.phoneNumber})
                    .UpdateColumns(n=>new {n.Phone}).Where(n=>n.WXID== openid).ExecuteCommandAsync();
            }

            return wXGetPhoneDto;
        }

        /// <summary>
        /// 获取accessToken
        /// </summary>
        /// <returns></returns>
        public async Task<AccessTokenResponse> GetAccessToken()
        {
            logger.LogInformation("调用GetAccessToken");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["appid"] = configuration["WXConf:appid"];
            query["secret"] = configuration["WXConf:secret"];
            query["grant_type"] = "client_credential";
            string url = configuration["WXURL:getAccessToken"] + "?" + query.ToString();
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            logger.LogInformation($"调用GetAccessToken接口,微信官方返回： {responseBody}");
            AccessTokenResponse wxresponse = JsonConvert.DeserializeObject<AccessTokenResponse>(responseBody);
            cacheService.Set(HardCode.CacheWachatAPPAccessToken, wxresponse.access_token);
            return wxresponse;
        }

        /// <summary>
        /// 从缓存中获取accessToken
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenByCache()
        {
            string token = cacheService.Get(HardCode.CacheWachatAPPAccessToken);
            if (string.IsNullOrWhiteSpace(token))
            {
                var resuponse = await GetAccessToken();
                return resuponse.access_token;
            }
            else
            {
                return token;
            }


        }


        /// <summary>
        /// 解密微信数据
        /// </summary>
        /// <param name="encryptedData">加密的数据</param>
        /// <param name="encryptIv">iv向量</param>
        /// <param name="sessionKey">调用 wx auth.code2Session 来获得</param>
        /// <returns></returns>
        private  string WechatDecrypt(string encryptedData, string encryptIv, string sessionKey)
        {
            //base64解码为字节数组
            var encryptData = Convert.FromBase64String(encryptedData);
            var key = Convert.FromBase64String(sessionKey);
            var iv = Convert.FromBase64String(encryptIv);

            //创建aes对象
            var aes = Aes.Create();

            if (aes == null)
            {
                throw new InvalidOperationException("未能获取Aes算法实例");
            }
            //设置模式为CBC
            aes.Mode = CipherMode.CBC;
            //设置Key大小
            aes.KeySize = 128;
            //设置填充
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            //创建解密器
            var de = aes.CreateDecryptor();
            //解密数据
            var decodeByteData = de.TransformFinalBlock(encryptData, 0, encryptData.Length);
            //转换为字符串
            var data = Encoding.UTF8.GetString(decodeByteData);
            
            return data;
        }


        #region 微信支付接口



        #endregion

    }
}
