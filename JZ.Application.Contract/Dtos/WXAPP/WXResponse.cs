//微信小程序服务返回类
namespace JZ.Application.Contract.Dtos.WXAPP
{


    public class WXResponse
    {
        public string errmsg { get; set; }

        public int errcode { get; set; }
    }

    public class WXLoginResponse : WXResponse
    {
        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get;set; }

        /// <summary>
        /// 平台唯一标识
        /// </summary>
        public string unionid { get; set; }

        /// <summary>
        /// 微信小程序ID
        /// </summary>
        public string openid { get; set; }

    }


    /// <summary>
    /// accessToken
    /// </summary>
    public class AccessTokenResponse
    {

        public string access_token { get; set; }

        public int expires_in { get; set; }

    }
}
