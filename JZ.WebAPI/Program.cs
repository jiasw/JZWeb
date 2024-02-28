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


            //ע����־��
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

            // Add services to the container.
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                #region ����API�ĵ�˵��
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "��������ӿ�",
                    Description = "��������΢��С����ӿ�",
                    Contact = new OpenApiContact
                    {
                        Name = "С����",
                        Email = "jiasw8@163.com"
                    }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "JZ.WebAPI.xml");
                config.IncludeXmlComments(xmlPath, true);
                #endregion


            });
            builder.Services.AddHttpContextAccessor();
            //ע�����
            ServiceInjection.Injection(builder,logger);
            // ע����������ݼ� `appsettings.json` �ļ���
            builder.Services.AddOptions();
            builder.Services.Configure<Options.TenpayOptions>(builder.Configuration.GetSection(nameof(Options.TenpayOptions)));
            //ע����Ѷ�ʼ�����
            builder.Services.Configure<TencentMailConf>(builder.Configuration.GetSection(nameof(TencentMailConf)));
            // ע�빤�� HTTP �ͻ���
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<Services.HttpClients.IWechatTenpayCertificateManagerFactory, Services.HttpClients.Implements.WechatTenpayCertificateManagerFactory>();
            builder.Services.AddSingleton<Services.HttpClients.IWechatTenpayHttpClientFactory, Services.HttpClients.Implements.WechatTenpayHttpClientFactory>();

            // ע���̨����
            builder.Services.AddHostedService<Services.BackgroundServices.TenpayCertificateRefreshingBackgroundService>();
            
            //�����������
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("cors", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            #region ע��ѩ��id
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