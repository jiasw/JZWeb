using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JZ.WebMange.Models;
using IdentityModel;
using JZ.Application.Contract.Dtos.Business;
using JZ.Application.Business;
using Microsoft.AspNetCore.Authorization;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 登录管理
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly SysMangerUserService sysMangerUserService;

        public AccountController(SysMangerUserService sysMangerUserService)
        {
            this.sysMangerUserService = sysMangerUserService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login([FromQuery] string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied([FromQuery] string returnUrl = null)
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel input)
        {
            ViewBag.ReturnUrl = input.ReturnUrl;

            MangerDto dto = await sysMangerUserService.GetMangerDto(new MangerDto() { LoginName = input.UserName, LoginPsw = input.Password });
            // 用户名密码相同视为登录成功
            if (dto==null)
            {
                ModelState.AddModelError("UserNameOrPasswordError", "无效的用户名或密码");
            }



            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, JwtClaimTypes.Name, JwtClaimTypes.Role);
            identity.AddClaims(new[]
            {
                new Claim(JwtClaimTypes.Id, Guid.NewGuid().ToString("N")),
                new Claim(JwtClaimTypes.Name, dto.LoginName)
            });

            var principal = new ClaimsPrincipal(identity);

            // 登录
            var properties = new AuthenticationProperties
            {
                IsPersistent = input.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14),
                AllowRefresh = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            if (Url.IsLocalUrl(input.ReturnUrl))
            {
                return Redirect(input.ReturnUrl);
            }

            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
    }
}
