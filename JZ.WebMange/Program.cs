using JZ.Application.Contract.Dtos.Conf;
using JZ.WebMange.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.FileProviders;
using NLog;
using NLog.Web;

namespace JZ.WebMange
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            var builder = WebApplication.CreateBuilder(args);

            //注入日志类
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

            //注入服务
            ServiceInjection.Injection(builder, logger);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
                .Configure<IDataProtectionProvider>((options,dp) => {
                    options.LoginPath = new PathString("/Account/Login");
                    options.LogoutPath = new PathString("/Account/Logout");
                    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                    options.ReturnUrlParameter = "returnUrl";

                    options.ExpireTimeSpan = TimeSpan.FromDays(14);
                    //options.Cookie.Expiration = TimeSpan.FromMinutes(30);
                    //options.Cookie.MaxAge = TimeSpan.FromDays(14);
                    options.SlidingExpiration = true;

                    options.Cookie.Name = "auth";
                    //options.Cookie.Domain = ".xxx.cn";
                    options.Cookie.Path = "/";
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.Cookie.IsEssential = true;
                    options.CookieManager = new ChunkingCookieManager();

                    options.DataProtectionProvider ??= dp;
                    var dataProtector = options.DataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware", CookieAuthenticationDefaults.AuthenticationScheme, "v2");
                    options.TicketDataFormat = new TicketDataFormat(dataProtector);

                    options.Events.OnSigningIn = context =>
                    {
                        Console.WriteLine($"{context.Principal.Identity.Name} 正在登录...");
                        return Task.CompletedTask;
                    };

                    options.Events.OnSignedIn = context =>
                    {
                        Console.WriteLine($"{context.Principal.Identity.Name} 已登录");
                        return Task.CompletedTask;
                    };

                    options.Events.OnSigningOut = context =>
                    {
                        Console.WriteLine($"{context.HttpContext.User.Identity.Name} 注销");
                        return Task.CompletedTask;
                    };

                    options.Events.OnValidatePrincipal += context =>
                    {
                        Console.WriteLine($"{context.Principal.Identity.Name} 验证 Principal");
                        return Task.CompletedTask;
                    };


                });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.InvokeHandlersAfterFailure = true;
            });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "upload")),
                RequestPath = "/upload",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=36000");
                }
            });

            app.UseStaticFiles();

            app.UseRouting();

           

            // 身份认证中间件
            app.UseAuthentication();

            // 授权中间件
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Urls.Add(builder.Configuration.GetSection("HostUrl").Get<string>());
            app.Run();
        }
    }
}