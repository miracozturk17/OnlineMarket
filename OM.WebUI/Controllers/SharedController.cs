using System;
using System.IO;
using System.Web.Mvc;

namespace OM.WebUI.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult UploadImage()
        {
            JsonResult result = GetResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(GetFile(file).FileName);

                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);

                file?.SaveAs(path);

                result.Data = new
                {
                    Success = true,
                    ImageUrl = $"/Content/images/{fileName}"
                };
            }
            catch (Exception e)
            {
                result.Data = new { Success = false, e.Message };
            }
            return result;
        }

        private static System.Web.HttpPostedFileBase GetFile(System.Web.HttpPostedFileBase file)
        {
            return file;
        }

        private static JsonResult GetResult()
        {
            return new JsonResult();
        }
    }
}