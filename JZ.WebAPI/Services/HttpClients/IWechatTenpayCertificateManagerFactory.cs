namespace JZ.WebAPI.Services.HttpClients
{
    using SKIT.FlurlHttpClient.Wechat.TenpayV3.Settings;

    public interface IWechatTenpayCertificateManagerFactory
    {
        CertificateManager Create(string merchantId);
    }
}
