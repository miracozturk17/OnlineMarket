﻿using System.Linq;
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
        private ProductSearchViewModel _model;

        public ProductController(ProductService productService, CategoryService categoryService, ProductSearchViewModel productSearchViewModel, ProductSearchViewModel model)
        {
            _productService = productService;
            _categoryService = categoryService;
            _model = model;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string searchtextcontroller, int? pageNo)
        {
            _model.PageNo = pageNo.HasValue ? pageNo.Value >0 ? pageNo.Value :1 : 1;

            var products = _productService.ProductList(_model.PageNo);

            if (string.IsNullOrEmpty(searchtextcontroller) == false)
            {
                _model.Searchterm = searchtextcontroller;
                _model.Products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(searchtextcontroller.ToLower())).ToList();
            }

            return PartialView(_model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = _categoryService.CategoryList();

            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            /*
             Service üzerinden de hareket edilebilir.
            _productService.ProductSave(product); 
            */

            var newProduct = new Product
            {
                Name = model.Name,
                Description = model.Description,
                UnitePrice = model.Price,
                //CategoryId = model.CategoryId
                Category = _categoryService.GetCategory(model.CategoryId)
            };

            _productService.ProductSave(newProduct);

            return RedirectToAction("ProductTable");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _productService.GetProduct(id);

            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            _productService.UpdateProduct(product);

            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);

            return RedirectToAction("ProductTable");
        }
    }
}