using System.Web.Mvc;
using OM.Services.Services;
using OM.WebUI.Models;

namespace OM.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryService _categoryService;

        public HomeController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            { FeaturedCategories = _categoryService.CategoryList() };

            return View(model);
        } 
    }
}