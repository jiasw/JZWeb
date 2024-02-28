using NLog.Web;
using Microsoft.Extensions.Logging;
using SqlSugar;
using MySqlConnector.Logging;
using JZ.Domain.Repository;
using JZ.Domain.Entitys;
using JZ.Application.Business;
using JZ.Application.Contract;
using JZ.Application.WXAPP;
using JZ.Application.Contract.Infrastructure;
using JZ.Application.Infrastructure;

namespace JZ.WebAPI.Common
{
    /// <summary>
    /// 服务注入
    /// </summary>
    public static class ServiceInjection
    {
        public static void Injection(WebApplicationBuilder builder, NLog.Logger logger)
        {
            
            
            //注入sqlsugar
            builder.Services.AddSingleton<ISqlSugarClient>(s =>
            {
                //Scoped用SqlSugarClient 
                SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.MySql,
                    ConnectionString = builder.Configuration.GetConnectionString("MysqlConnect"),
                    IsAutoCloseConnection = true,
                },
               db =>
               {
                   //过滤器
                   db.QueryFilter.AddTableFilter<JZ_YW_PIC>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_YW_ADDR>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_YW_DTGL>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_YW_FWXX>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_YW_LXWM>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_YW_ORDER>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_YW_PAYINFO>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_SYS_MANGER>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_YW_USERINFO>(it => it.Del == 0);
                   db.QueryFilter.AddTableFilter<JZ_SYS_ZDXX>(it => it.Del == 0);

                   //单例参数配置，所有上下文生效
                   db.Aop.OnLogExecuting = (sql, pars) =>
                   {
                       Console.WriteLine(sql);
                       logger.Info($"生成sql：{sql}");
                       //获取IOC对象不要求在一个上下文
                       //vra log=s.GetService<Log>()

                       //获取IOC对象要求在一个上下文
                       //var appServive = s.GetService<IHttpContextAccessor>();
                       //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
                   };
               });
                return sqlSugar;
            });
            //builder.Services.AddScoped(typeof(Repository<>));
            builder.Services.AddSingleton(typeof(BannerRepository));
            builder.Services.AddSingleton(typeof(AddressRepository));
            builder.Services.AddSingleton(typeof(AssessReository));
            builder.Services.AddSingleton(typeof(DTGLRepository));
            builder.Services.AddSingleton(typeof(FWXXRepository));
            builder.Services.AddSingleton(typeof(LXWMRepository));
            builder.Services.AddSingleton(typeof(OrderRepository));
            builder.Services.AddSingleton(typeof(PayInfoRepository));
            builder.Services.AddSingleton(typeof(SysMangerRepository));
            builder.Services.AddSingleton(typeof(UserInfoRepository));
            builder.Services.AddSingleton(typeof(ZDXXRepository));
            //注入automapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            //注入业务服务
            builder.Services.AddSingleton(typeof(BannerService));
            builder.Services.AddSingleton(typeof(AddressService));
            builder.Services.AddSingleton(typeof(AssessService));
            builder.Services.AddSingleton(typeof(BannerService));
            builder.Services.AddSingleton(typeof(DTGLService));
            builder.Services.AddSingleton(typeof(FWXXService));
            builder.Services.AddSingleton(typeof(LXWMService));
            builder.Services.AddSingleton(typeof(OrderService));
            builder.Services.AddSingleton(typeof(PayInfoService));
            builder.Services.AddSingleton(typeof(SysMangerUserService));
            builder.Services.AddSingleton(typeof(UserInfoService));
            builder.Services.AddSingleton(typeof(ZDXXService));

            //注入缓存服务
            builder.Services.AddSingleton<JZ.Application.Contract.Infrastructure.ICacheService, JZ.Application.Infrastructure.MemoryCacheService>();

            //注入小程序服务
            builder.Services.AddSingleton(typeof(WachatMicroAppService));
            //注入发送邮件服务
            builder.Services.AddSingleton<IMailNotifyService, TencentMailNotifyService>();
            //注入定时服务
            builder.Services.AddHostedService<GetAccessTokenJob>();
        }
    }
}
