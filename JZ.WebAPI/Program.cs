using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using JZ.Application.Contract.Dtos.Conf;
using JZ.WebAPI.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Text;
using Yitter.IdGenerator;

namespace JZ.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            var builder = WebApplication.CreateBuilder(args);
            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            //注入日志类
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

            // Add services to the container.
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                #region 配置API文档说明
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "家政服务接口",
                    Description = "家政服务微信小程序接口",
                    Contact = new OpenApiContact
                    {
                        Name = "小马帮帮",
                        Email = "jiasw8@163.com"
                    }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "JZ.WebAPI.xml");
                config.IncludeXmlComments(xmlPath, true);
                #endregion


            });
            builder.Services.AddHttpContextAccessor();
            //注入服务
            ServiceInjection.Injection(builder,logger);
            // 注入配置项（内容见 `appsettings.json` 文件）
            builder.Services.AddOptions();
            builder.Services.Configure<Options.TenpayOptions>(builder.Configuration.GetSection(nameof(Options.TenpayOptions)));
            //注入腾讯邮件配置
            builder.Services.Configure<TencentMailConf>(builder.Configuration.GetSection(nameof(TencentMailConf)));
            // 注入工厂 HTTP 客户端
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<Services.HttpClients.IWechatTenpayCertificateManagerFactory, Services.HttpClients.Implements.WechatTenpayCertificateManagerFactory>();
            builder.Services.AddSingleton<Services.HttpClients.IWechatTenpayHttpClientFactory, Services.HttpClients.Implements.WechatTenpayHttpClientFactory>();

            // 注入后台任务
            builder.Services.AddHostedService<Services.BackgroundServices.TenpayCertificateRefreshingBackgroundService>();
            
            //配置允许跨域
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("cors", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            #region 注入雪花id
            var options = new IdGeneratorOptions(1);
            YitIdHelper.SetIdGenerator(options);
            #endregion


            var app = builder.Build();
            if (builder.Configuration["Dev"]== "True")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.CustomException();
            app.UseCors("cors");
            app.MapControllers();
            app.Urls.Add(builder.Configuration.GetSection("HostUrl").Get<string>());
            app.Run();
        }
    }
}