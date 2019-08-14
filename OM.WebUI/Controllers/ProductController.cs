using System.Web.Mvc;
using OM.Entities.EntityClass;
using OM.Services.Services;
using OM.WebUI.Models;

namespace OM.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ConfigrationService _configrationService;
        private readonly ProductSearchViewModel _model;
        private readonly NewProductViewModel _newModel;
        private readonly EditProductViewModel _editModel;

        public ProductController(ProductService productService, ConfigrationService configrationService,
            CategoryService categoryService, ProductSearchViewModel productSearchViewModel,
            ProductSearchViewModel model, NewProductViewModel newModel,
            EditProductViewModel editModel)
        {
            _productService = productService;
            _categoryService = categoryService;
            _configrationService = configrationService;
            _newModel = newModel;
            _editModel = editModel;
            _model = model;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            var pageSize = _configrationService.PageSize();

            _model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = _productService.GetProductsCount(search);

            _model.Products = _productService.GetProducts(search, pageNo.Value, pageSize);

            _model.Pager = new BaseListingViewmodel(totalRecords, pageNo, pageSize);

            return PartialView(_model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            _newModel.AvailableCategories = _categoryService.CategoryList();

            return PartialView(_newModel);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            var newProduct = new Product
            {
                Name = model.Name,
                Description = model.Description,
                UnitePrice = model.Price,
                Category = _categoryService.GetCategory(model.CategoryId),
                ImageUrl = model.ImageUrl
            };

            _productService.ProductSave(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var product = _productService.GetProduct(Id);

            _editModel.Id= product.Id;
            _editModel.Name = product.Name;
            _editModel.Description = product.Description;
            _editModel.Price = product.UnitePrice;
            _editModel.CategoryId = product.Category?.Id ?? 0;
            _editModel.ImageUrl = product.ImageUrl;

            _editModel.AvailableCategories = _categoryService.CategoryList();

            return PartialView(_editModel);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = _productService.GetProduct(model.Id);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.UnitePrice = model.Price;
            existingProduct.Category = null; 
            existingProduct.CategoryId = model.CategoryId;

            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                existingProduct.ImageUrl = model.ImageUrl;
            }

            _productService.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = _productService.GetProduct(ID);

            if (model.Product == null) return HttpNotFound();

            return View(model);
        }
    }
}