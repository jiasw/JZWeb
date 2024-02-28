using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JZ.WebMange.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public int PageSize = 10;

        public IActionResult ReturnBack(string action="Index")
        {
            string returnUrl = Request.Query["returnUrl"];
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction(action);
            }
            else
            {
                return Redirect(action+returnUrl);
            }

        }

    }
}
