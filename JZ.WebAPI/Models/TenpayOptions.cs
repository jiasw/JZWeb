using System;
using Microsoft.Extensions.Options;

namespace JZ.WebAPI.Options
{
    public partial class TenpayOptions : IOptions<TenpayOptions>
    {
        TenpayOptions IOptions<TenpayOptions>.Value => this;

        public Types.WechatMerchant[] Merchants { get; set; } = Array.Empty<Types.WechatMerchant>();

        public string NotifyUrl { get; set; } = string.Empty;
    }

    public partial class TenpayOptions
    {
        public static class Types
        {
            public class WechatMerchant
            {
                public string MerchantId { get; set; } = string.Empty;

                public string SecretV3 { get; set; } = string.Empty;

                public string CertificateSerialNumber { get; set; } = string.Empty;

                public string CertificatePrivateKey { get; set; } = string.Empty;
            }
        }
    }
}
