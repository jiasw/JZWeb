using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class LXWMController : BaseController
    {
        private readonly LXWMService lXWMService;

        public LXWMController(LXWMService lXWMService)
        {
            this.lXWMService = lXWMService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Model= await lXWMService.GetModel();
            if (Model == null)
            {
                Model = new Application.Contract.Dtos.Business.LXWMDto();
            }
            return View(Model);
        }

        [HttpPost]
        public async Task<ApiResponse> Save([FromForm] JZ.Application.Contract.Dtos.Business.LXWMDto dto)
        {
            var result = await lXWMService.Save(dto);
            if (result)
            {
                return ApiResponse.Success("保存成功");
            }
            else
            {
                return ApiResponse.Fail("保存失败");
            }
        }
    }
}
