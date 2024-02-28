using SKIT.FlurlHttpClient.Wechat.TenpayV3;

namespace JZ.WebAPI.Services.HttpClients
{
    public interface IWechatTenpayHttpClientFactory
    {
        WechatTenpayClient Create(string merchantId);
    }
}
