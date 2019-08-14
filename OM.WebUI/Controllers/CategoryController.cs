using System.Web.Mvc;
using OM.Entities.EntityClass;
using OM.Services.Services;
using OM.WebUI.Models;

namespace OM.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly CategoryViewModel _model = new CategoryViewModel();
        private readonly NewCategoryViewModel _newModel = new NewCategoryViewModel();
        private readonly EditCategoryViewModel _editModel = new EditCategoryViewModel();

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
            return View(_newModel);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    IsFeatured = model.IsFeatured
                };

                _categoryService.CategorySave(newCategory);

                return RedirectToAction("CategoryTable");
            }
            return new HttpStatusCodeResult(500);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var category = _categoryService.GetCategory(Id);

            _editModel.Id = category.Id;
            _editModel.Name = category.Name;
            _editModel.Description = category.Description;
            _editModel.ImageUrl = category.ImageUrl;
            _editModel.IsFeatured = category.IsFeatured;

            return PartialView(_editModel);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = _categoryService.GetCategory(model.Id);

            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageUrl = model.ImageUrl;
            existingCategory.IsFeatured = model.IsFeatured;

            _categoryService.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
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

        public ActionResult CategoryTable(string search, int? pageNo)
        {
            _model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = _categoryService.GetCategoriesCount(search);
            _model.Categories = _categoryService.GetCategories(search, pageNo.Value);

            if (_model.Categories != null)
            {
                _model.Pager = new BaseListingViewmodel(totalRecords, pageNo, 3);

                return PartialView("CategoryTable", _model);
            }
            return HttpNotFound();
        }
    }
}