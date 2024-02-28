using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Xml.Linq;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 字典信息管理
    /// </summary>
    public class ZDXXController : BaseController
    {
        private readonly ILogger<MangerController> logger;
        private readonly ZDXXService zDXXService;

        public ZDXXController(ILogger<MangerController> logger, ZDXXService zDXXService) {
            this.logger = logger;
            this.zDXXService = zDXXService;
        }

        [HttpGet]
        public async Task< IActionResult> Index(string ZDFLBM, int pageIndex = 1)
        {
            pageIndex= pageIndex <= 1 ? 1 : pageIndex; 
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex;
            var pagelist = await zDXXService.GetListByPage(pageIndex, PageSize, ZDFLBM);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);

            var zffllist = await zDXXService.GetZDLXList();
            ViewBag.FLlist = zffllist;

            return View(pagelist.Data);
            
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            JZ.Application.Contract.Dtos.Business.ZDXXDto dto= new Application.Contract.Dtos.Business.ZDXXDto();

            if (id>0)
            {
                dto = await zDXXService.GetZDModel(id);
            }
            
            var zffllist = await zDXXService.GetZDLXList();
            ViewBag.FLlist = zffllist;

            return View(dto);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] JZ.Application.Contract.Dtos.Business.ZDXXDto zdxxDto)
        {
            var result = await zDXXService.Save(zdxxDto);
            return ReturnBack();
        }

        [HttpPost]
        public async Task<ApiResponse> Delete(string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await zDXXService.Delete(longids);
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
