using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 评价管理
    /// </summary>
    public class AssessController : BaseController
    {
        private readonly ILogger<AssessController> logger;
        private readonly AssessService assessService;
        private readonly ZDXXService zDXXService;

        public AssessController(ILogger<AssessController> logger,AssessService assessService,ZDXXService zDXXService)
        {
            this.logger = logger;
            this.assessService = assessService;
            this.zDXXService = zDXXService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, string isgood = "")
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist = await assessService.GetListByPage(pageIndex, PageSize, keyword,isgood);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            if (pagelist.Data!=null)
            {
                foreach (var item in pagelist.Data)
                {
                    item.IsGood = await zDXXService.GetZDMC("000002", item.IsGood);
                }
            }
            ViewBag.FLlist = await zDXXService.GetZDXXListByLXBM("000002");
            return View(pagelist.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var dto = await assessService.GetModel(id);
            if (dto == null)
            {
                dto = new Application.Contract.Dtos.Business.AssessDto();
            }
            ViewBag.FLlist = await zDXXService.GetZDXXListByLXBM("000002");
            return View(dto);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] JZ.Application.Contract.Dtos.Business.AssessDto assessDto)
        {
            var result = await assessService.Save(assessDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ApiResponse> Delete(string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await assessService.Delete(longids);
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
