using JZ.Application.Business;
using JZ.Application.Contract.Dtos.Business;
using JZ.WebMange.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 主页管理
    /// </summary>
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OrderService orderService;

        public HomeController(ILogger<HomeController> logger,OrderService orderService)
        {
            _logger = logger;
            this.orderService = orderService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task< IActionResult> Index()
        {
            //获取订单信息
            HomoOrderDto dto = await orderService.GetHomeOrderState();
            ViewBag.dto=dto;
            return View();
        }
        [HttpGet]
        
        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}