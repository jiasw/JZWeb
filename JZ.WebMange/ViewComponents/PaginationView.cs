using Microsoft.AspNetCore.Mvc;

namespace JZ.WebMange.ViewComponents
{
    
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
