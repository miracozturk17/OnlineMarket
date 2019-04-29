using System.Web.Mvc;
using OM.Entities.EntityClass;
using OM.Services.Services;

namespace OM.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController()
        {
                
        }

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var categories = _categoryService.CategoryList();

            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            _categoryService.CategorySave(category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategory(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _categoryService.UpdateCategory(category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetCategory(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            _categoryService.DeleteCategory(category.Id);

            return RedirectToAction("Index");
        }
    }
}