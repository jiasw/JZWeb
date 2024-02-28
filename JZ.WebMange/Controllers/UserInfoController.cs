using JZ.Application.Business;
using JZ.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JZ.WebMange.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserInfoController : BaseController
    {
        private readonly ILogger<UserInfoController> logger;
        private readonly UserInfoService userInfoService;
        private readonly ZDXXService zDXXService;

        public UserInfoController(ILogger<UserInfoController> logger,  UserInfoService userInfoService,ZDXXService zDXXService)
        {
            this.logger = logger;
            this.userInfoService = userInfoService;
            this.zDXXService = zDXXService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1)
        {
            ViewBag.TotalPageCount = 1;
            ViewBag.CurrentPageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var pagelist = await userInfoService.GetListByPage(pageIndex, PageSize, keyword);
            var tmpTotalCount = pagelist.TotalCount;
            ViewBag.TotalPageCount = (tmpTotalCount / PageSize) + (tmpTotalCount % PageSize == 0 ? 0 : 1);
            if (pagelist.Data!=null)
            {
                foreach (var item in pagelist.Data)
                {
                    item.Sex= await zDXXService.GetZDMC("000001", item.Sex);
                    item.YHLY= await zDXXService.GetZDMC("000008", item.Sex);

                }
            }
            
            return View(pagelist.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var dto = await userInfoService.GetModel(id);
            if (dto == null)
            {
                dto = new Application.Contract.Dtos.Business.UserInfoDto();
            }
            ViewBag.XBlist = await zDXXService.GetZDXXListByLXBM("000001");
            ViewBag.LBlist = await zDXXService.GetZDXXListByLXBM("000008");
            return View(dto);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Save([FromForm] JZ.Application.Contract.Dtos.Business.UserInfoDto userInfoDto)
        {
            var result = await userInfoService.Save(userInfoDto);
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
            var result = await userInfoService.Delete(longids);
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
