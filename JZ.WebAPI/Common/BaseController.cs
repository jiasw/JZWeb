using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JZ.WebAPI.Common
{
    /// <summary>
    /// 后台管理控制器基类
    /// </summary>
    [ApiController, Route("api/[controller]")]
    public class BaseController : ControllerBase
    {


    }
}
