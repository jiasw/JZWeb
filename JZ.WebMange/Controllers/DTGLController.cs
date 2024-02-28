using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using JZ.Application.Contract.Dtos.Business;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 动态管理
    /// </summary>
    public class DTGLController : BaseController
    {
        private readonly DTGLService dTGLService;

        public DTGLController(DTGLService dTGLService)
        {
            this.dTGLService = dTGLService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist = await dTGLService.GetListByPage( pageIndex, PageSize, keyword);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            return View(pagelist.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id = 0)
        {
            var model= await dTGLService.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public async Task<ApiResponse> Delete([FromForm] string ids)
        {
            var longids = ids.Split(',').Select(s => long.Parse(s));
            var result = await dTGLService.Delete(longids);
            if (result)
            {
                return ApiResponse.Success("删除成功");
            }
            else
            {
                return ApiResponse.Fail("删除失败");
            }
        }
        [HttpPost]
        public async Task<ApiResponse> Save([FromForm] Application.Contract.Dtos.Business.DTGLDto dTGLDto)
        {
            var result = await dTGLService.Save(dTGLDto);
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
