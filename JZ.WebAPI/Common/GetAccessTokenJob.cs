using JZ.Application.Contract.Infrastructure;
using JZ.Application.WXAPP;
using JZ.WebAPI.Controllers;
using System.Web;

namespace JZ.WebAPI.Common
{
    public class GetAccessTokenJob : BackgroundService
    {
        private readonly ILogger<GetAccessTokenJob> logger;
        private readonly ICacheService cacheService;
        private readonly IConfiguration configuration;
        private readonly WachatMicroAppService wachatMicroAppService;
        private readonly HttpClient client = new HttpClient();
        private readonly int expireSecond = 7200;//默认过期时间为7200

        public GetAccessTokenJob(ILogger<GetAccessTokenJob> logger, ICacheService cacheService,IConfiguration configuration, WachatMicroAppService wachatMicroAppService )
        {
            this.logger = logger;
            this.cacheService = cacheService;
            this.configuration = configuration;
            this.wachatMicroAppService = wachatMicroAppService;
            expireSecond = int.Parse(configuration["WXConf:accessTokenExpires"]??"7000");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await new TaskFactory().StartNew(async () => {
                    try
                    {
                        logger.LogInformation($"定时调用GetAccess接口");
                        var response= await wachatMicroAppService.GetAccessToken();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "定时获取accessToken出错！");
                    }
                
                
                });
                Thread.Sleep(1000 * expireSecond);
            }
        }
    }
}
