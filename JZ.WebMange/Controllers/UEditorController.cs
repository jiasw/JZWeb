using JZ.Application.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UEditor.Core;

namespace JZ.WebMange.Controllers
{
    public class UEditorController : Controller
    {
        private readonly UEditorService _ueditorService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string uploadpath = "upload";
        private readonly string nomalimages = "dtfm";

        public UEditorController(UEditorService ueditorService,IWebHostEnvironment webHostEnvironment)
        {
            this._ueditorService = ueditorService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet, HttpPost]
        public ContentResult Upload()
        {
            var response = _ueditorService.UploadAndGetResponse(HttpContext);
            return Content(response.Result, response.ContentType);
        }

        
        [HttpPost]
        public async Task<ContentResult> UpLoadImage()
        {
            string result = await UploadFile(nomalimages);
            return Content(result);
        }


        private async Task<string> UploadFile(string filetype)
        {
            var savepath = System.IO.Path.Combine(uploadpath, filetype);
            var fullpath = Path.Combine(webHostEnvironment.ContentRootPath, savepath);
            if (!Directory.Exists(fullpath))
            {
                Directory.CreateDirectory(fullpath);
            }
            var formFile = HttpContext.Request.Form.Files[0];
            string ext = Path.GetExtension(formFile.FileName);
            string newFileName = string.Concat(Utils.GetDateMillsecondStr(), Utils.GetOrderCode(), ext);
            var fullfulename = Path.Combine(fullpath, newFileName);
            string result = Path.Combine(savepath, newFileName);
            using (var stream = System.IO.File.Create(fullfulename))
            {
                await formFile.CopyToAsync(stream);
            }
            return "/"+result;

        }
    }
}
