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

        public ActionResult ProductTable(string aramametnicontroller)
        {
            var products = _productService.ProductList();

            if (string.IsNullOrEmpty(aramametnicontroller) == false)
            {
                products = products.Where(p =>p.Name!=null && p.Name.ToLower().Contains(aramametnicontroller.ToLower())).ToList();
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
    }
}