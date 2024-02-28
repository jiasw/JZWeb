using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Dtos.Business;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 服务信息管理
    /// </summary>
    public class FWXXController : BaseController
    {
        private readonly ILogger<MangerController> logger;
        private readonly FWXXService fWXXService;
        private readonly ZDXXService zDXXService;

        public FWXXController(ILogger<MangerController> logger, FWXXService fWXXService,ZDXXService zDXXService)
        {
            this.logger = logger;
            this.fWXXService = fWXXService;
            this.zDXXService = zDXXService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string lxbm, int pageIndex = 1)
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist = await fWXXService.GetListByPage(pageIndex, PageSize, lxbm);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            foreach (var fwModel in pagelist.Data)
            {
                fwModel.FWLXMC = await zDXXService.GetZDMC("000003", fwModel.FWLX);
                fwModel.UnitMC= await zDXXService.GetZDMC("000004", fwModel.Unit);
                fwModel.IsGood = await zDXXService.GetZDMC("000002", fwModel.IsGood);
            }
            ViewBag.FLlist = await zDXXService.GetZDXXListByLXBM("000003");
            return View(pagelist.Data);
        }

        public async Task< IActionResult> Details(long id=0)
        {
            var dto=new FWXXDto();
            if (id>0)
            {
                dto = await fWXXService.GetModel(id);
            }
            
            ViewBag.FWLXList = await zDXXService.GetZDXXListByLXBM("000003");
            ViewBag.DWList = await zDXXService.GetZDXXListByLXBM("000004");
            ViewBag.SFList = await zDXXService.GetZDXXListByLXBM("000002");
            return View(dto);
        }

        [HttpPost]
        public async Task<ApiResponse> Save(FWXXDto dto)
        {
            if (dto.listBanner != null && dto.listBanner.Count() > 0)
            {
                dto.Banner = string.Join(';', dto.listBanner.Where(n => !string.IsNullOrWhiteSpace(n)));
            }
                    
            var result = await fWXXService.Save(dto);
            if (result)
            {
                return ApiResponse.Success("保存成功");
            }
            else
            {
                return ApiResponse.Fail("保存失败");
            }
        }

        [HttpPost]
        public async Task<ApiResponse> Delete(string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await fWXXService.Delete(longids);
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
