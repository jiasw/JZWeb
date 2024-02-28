using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 管理员信息
    /// </summary>
    public class MangerController : BaseController
    {
        private readonly ILogger<MangerController> logger;
        private readonly SysMangerUserService sysMangerUserService;

        public MangerController(ILogger<MangerController> logger, SysMangerUserService sysMangerUserService)
        {
            this.logger = logger;
            this.sysMangerUserService = sysMangerUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name,int pageIndex=1)
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist= await sysMangerUserService.GetPageDtoAsync(name, pageIndex,PageSize);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            return View(pagelist.Data);
        }
        [HttpGet]
        public async Task< IActionResult> Edit(long id=0)
        {
            JZ.Application.Contract.Dtos.Business.MangerDto dto=new Application.Contract.Dtos.Business.MangerDto();
            if (id>0)
            {
                dto = await sysMangerUserService.GetModelByID(id);
            }
            return View(dto);
        }


        [HttpPost]
        public async Task<ApiResponse> Delete([FromForm] string ids)
        {
            var longids =ids.Split(',').Select(s => long.Parse(s) );
            var result= await sysMangerUserService.Delete(longids);
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
        public async Task<IActionResult> Save([FromForm] JZ.Application.Contract.Dtos.Business.MangerDto mangerDto)
        {
            var result= await sysMangerUserService.Save(mangerDto);
            return RedirectToAction("Index");
        }
    }
}
