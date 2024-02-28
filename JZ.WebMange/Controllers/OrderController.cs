using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JZ.WebMange.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> logger;
        private readonly OrderService orderService;
        private readonly ZDXXService zDXXService;

        public OrderController(ILogger<OrderController> logger, OrderService orderService, ZDXXService zDXXService)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.zDXXService = zDXXService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist = await orderService.GetListByPage(pageIndex, PageSize, keyword);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            foreach (var fwModel in pagelist.Data)
            {
                fwModel.OrderState = await zDXXService.GetZDMC("000005", fwModel.OrderState);
            }
            return View(pagelist.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var dto = await orderService.GetModel(id);
            if (dto == null)
            {
                dto = new Application.Contract.Dtos.Business.OrderDto();
            }
            return View(dto);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] JZ.Application.Contract.Dtos.Business.OrderDto orderDto)
        {
            var result = await orderService.Save(orderDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ApiResponse> Delete(string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await orderService.Delete(longids);
            if (result)
            {
                return ApiResponse.Success("删除成功");
            }
            else
            {
                return ApiResponse.Fail("删除失败");
            }
        }


        public async Task<IActionResult> SetOrderState(long orderid,string state)
        {
            var result= await orderService.MangeUpdateOrState(orderid,state);
            return RedirectToAction("Index");
        }
    }
}
