using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Domain.Shared
{
    public static class HardCode
    {
        /// <summary>
        /// banner 图缓存键
        /// </summary>
        public static string CacheBannerKey = "CacheBannerKey";

        /// <summary>
        /// 字典信息
        /// </summary>
        public static string CacheZDXXKey = "CacheZDXXKey";

        public static string CacheFWXXKey = "CacheFWXXKey";

        /// <summary>
        /// 管理员密码加密盐值
        /// </summary>

        public static byte[] MangerSalt = Convert.FromBase64String("8A2B6F7C98D51E9A3857436D0C9A1B2E");


        /// <summary>
        /// 微信小程序AccessToken缓存
        /// </summary>
        public static string CacheWachatAPPAccessToken = "CacheWachatAPPAccessToken";
    }
}
