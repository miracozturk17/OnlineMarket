using System.Linq;
using System.Web.Mvc;
using OM.Services.Services;
using OM.WebUI.Models;

namespace OM.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopViewModel _model=new ShopViewModel();

        private readonly ProductService _productService;

        public ShopController(ProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Checkout()
        {
            System.Web.HttpCookie cartProductsCookie = GetCookie();

            if (cartProductsCookie != null)
            {
                /*
                Parçalı halde adım adım kullanılabilir

                var productIDs = cartProductsCookie.Value;

                var ids = productIDs.Split('-');

                List<int> pIDs = ids.Select(int.Parse).ToList();
                */

                _model.CartProductIDs = cartProductsCookie.Value.Split('-').Select(int.Parse).ToList();

                _model.CartProducts = _productService.GetProducts(_model.CartProductIDs);
            }

            return View();
        }

        private System.Web.HttpCookie GetCookie()
        {
            return Request.Cookies["CartProducts"];
        }
    }
}