using JZ.Application.Business;
using JZ.Application.Contract;
using JZ.Application.Contract.Infrastructure;
using JZ.Application.Infrastructure;
using JZ.Domain.Entitys;
using JZ.Domain.Repository;
using SqlSugar;
using UEditor.Core;

namespace JZ.WebMange.Common
{
    public class ServiceInjection
    {
        public static void Injection(WebApplicationBuilder builder, NLog.Logger logger)
        {


            //注入sqlsugar
            builder.Services.AddScoped<ISqlSugarClient>(s =>
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
            //注入仓储服务
            //builder.Services.AddScoped(typeof(Repository<>));
            builder.Services.AddScoped(typeof(BannerRepository));
            builder.Services.AddScoped(typeof(AddressRepository));
            builder.Services.AddScoped(typeof(AssessReository));
            builder.Services.AddScoped(typeof(DTGLRepository));
            builder.Services.AddScoped(typeof(FWXXRepository));
            builder.Services.AddScoped(typeof(LXWMRepository));
            builder.Services.AddScoped(typeof(OrderRepository));
            builder.Services.AddScoped(typeof(PayInfoRepository));
            builder.Services.AddScoped(typeof(SysMangerRepository));
            builder.Services.AddScoped(typeof(UserInfoRepository));
            builder.Services.AddScoped(typeof(ZDXXRepository));

            //注入automapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            //注入业务服务
            builder.Services.AddScoped(typeof(AddressService));
            builder.Services.AddScoped(typeof(AssessService));
            builder.Services.AddScoped(typeof(BannerService));
            builder.Services.AddScoped(typeof(DTGLService));
            builder.Services.AddScoped(typeof(FWXXService));
            builder.Services.AddScoped(typeof(LXWMService));
            builder.Services.AddScoped(typeof(OrderService));
            builder.Services.AddScoped(typeof(PayInfoService));
            builder.Services.AddScoped(typeof(SysMangerUserService));
            builder.Services.AddScoped(typeof(UserInfoService));
            builder.Services.AddScoped(typeof(ZDXXService));

            //注入缓存服务
            builder.Services.AddSingleton<JZ.Application.Contract.Infrastructure.ICacheService, JZ.Application.Infrastructure.MemoryCacheService>();

            
            //注入uediter
            builder.Services.AddUEditorService();
        }
    }
}
