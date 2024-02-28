using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Dtos.Business;
using Microsoft.AspNetCore.Mvc;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// Banner图管理
    /// </summary>
    public class BannerController : BaseController
    {
        private readonly ILogger<MangerController> logger;
        private readonly BannerService bannerService;

        public BannerController(ILogger<MangerController> logger, BannerService bannerService)
        {
            this.logger = logger;
            this.bannerService = bannerService;
        }

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var list = await bannerService.GetBannerList();
            return View(list);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var dto= await bannerService.GetBannerDtoByID(id);
            if (dto==null)
            {
                dto = new Application.Contract.Dtos.Business.BannerDto();
            }
            return View(dto);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] JZ.Application.Contract.Dtos.Business.BannerDto bannerDto)
        {
            var result = await bannerService.Save(bannerDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ApiResponse> Delete(string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await bannerService.Delete(longids);
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
