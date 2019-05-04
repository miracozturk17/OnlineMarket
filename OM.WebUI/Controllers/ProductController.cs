using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using OM.Entities.EntityClass;
using OM.Services.Services;

namespace OM.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string searchtextcontroller)
        {
            var products = _productService.ProductList();

            if (string.IsNullOrEmpty(searchtextcontroller) == false)
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(searchtextcontroller.ToLower())).ToList();
            }

            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            _productService.ProductSave(product);

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