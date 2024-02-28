using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 用户地址管理
    /// </summary>
    public class AddressController : BaseController
    {
        private readonly AddressService addressService;
        private readonly ILogger<AddressController> logger;

        public AddressController(AddressService addressService, ILogger<AddressController> logger)
        {
            this.addressService = addressService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task< IActionResult> Index(string keyword, int pageIndex = 1)
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist = await addressService.GetListByPage(pageIndex, PageSize, keyword);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            return View(pagelist.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var dto = await addressService.GetModel(id);
            if (dto == null)
            {
                dto = new Application.Contract.Dtos.Business.AddressDto();
            }
            return View(dto);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] JZ.Application.Contract.Dtos.Business.AddressDto addressDto)
        {
            var result = await addressService.Save(addressDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ApiResponse> Delete(string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await addressService.Delete(longids);
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
