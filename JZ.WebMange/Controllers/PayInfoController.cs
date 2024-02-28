using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 支付信息
    /// </summary>
    public class PayInfoController : BaseController
    {
        private readonly ILogger<PayInfoController> logger;
        private readonly PayInfoService payInfoService;
        private readonly ZDXXService zDXXService;

        public PayInfoController(ILogger<PayInfoController> logger, PayInfoService payInfoService, ZDXXService zDXXService)
        {
            this.logger = logger;
            this.payInfoService = payInfoService;
            this.zDXXService = zDXXService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist = await payInfoService.GetListByPage(pageIndex, PageSize, keyword);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            foreach (var item in pagelist.Data)
            {
                item.PayType = await zDXXService.GetZDMC("000007", item.PayType);
                item.PayState = await zDXXService.GetZDMC("000006", item.PayState);
                item.CallState = await zDXXService.GetZDMC("000002", item.CallState);
            }
            return View(pagelist.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var dto = await payInfoService.GetModel(id);
            if (dto == null)
            {
                dto = new Application.Contract.Dtos.Business.PayInfoDto();
            }
            return View(dto);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] JZ.Application.Contract.Dtos.Business.PayInfoDto payInfoDto)
        {
            var result = await payInfoService.Save(payInfoDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ApiResponse> Delete(string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await payInfoService.Delete(longids);
            if (result)
            {
                return ApiResponse.Success("删除成功");
            }
            else
            {
                return ApiResponse.Fail("删除失败");
            }
        }
    }
}
