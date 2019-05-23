using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OM.Entities.EntityClass;
using OM.Services.Services;
using OM.WebUI.Models;

namespace OM.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopViewModel _model;
        private readonly ProductService _productService;
        private readonly ConfigrationService _configrationService;
        private readonly CategoryService _categoryService;
        private readonly ShopService _shopService;
        private readonly FilterProductsViewModel _filterModel;
        private readonly JsonResult _result;
        private readonly CheckoutViewModel _checkoutViewModel;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ShopController(ProductService productService, ConfigrationService configrationService,
            CategoryService categoryService, ShopViewModel model, FilterProductsViewModel filterModel, 
            CheckoutViewModel checkoutViewModel, JsonResult result, ShopService shopService)
        {
            _productService = productService;
            _configrationService = configrationService;
            _categoryService = categoryService;
            _model = model;
            _filterModel = filterModel;
            _shopService = shopService;
            _checkoutViewModel = checkoutViewModel;
            _result = result;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        private ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            set => _userManager = value;
        }

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = _configrationService.ShopPageSize();

            _model.SearchTerm = searchTerm;
            _model.FeaturedCategories = _categoryService.GetFeaturedCategory();
            _model.MaximumPrice = _productService.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            _model.SortBy = sortBy;
            _model.CategoryId = categoryID;

            int totalCount = _productService.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            _model.Products = _productService.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            _model.Pager = new BaseListingViewmodel(totalCount, pageNo, pageSize);

            return View(_model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = _configrationService.ShopPageSize();

            _filterModel.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            _filterModel.SortBy = sortBy;
            _filterModel.CategoryId= categoryID;

            int totalCount = _productService.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            _filterModel.Products = _productService.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            _filterModel.Pager = new BaseListingViewmodel(totalCount, pageNo, pageSize);

            return PartialView(_filterModel);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var cartProductsCookie = Request.Cookies["CartProducts"];

            if (cartProductsCookie != null && !string.IsNullOrEmpty(cartProductsCookie.Value))
            {
                _checkoutViewModel.CartProductIDs = cartProductsCookie.Value.Split('-').Select(int.Parse).ToList();

                _checkoutViewModel.CartProducts = _productService.GetProducts(_checkoutViewModel.CartProductIDs);

                _checkoutViewModel.User = UserManager.FindById(User.Identity.GetUserId());
            }

            return View(_checkoutViewModel);
        }

        public JsonResult PlaceOrder(string productIDs)
        {
            _result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIDs))
            {
                var productQuantities = productIDs.Split('-').Select(int.Parse).ToList();

                var boughtProducts = _productService.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order
                {
                    UserId = User.Identity.GetUserId(),
                    OrderedAt = DateTime.Now,
                    Status = "Pending",
                    TotalAmount = boughtProducts.Sum(x => x.UnitePrice * productQuantities.Count(productID => productID == x.Id)),

                    OrderItems = new List<OrderItem>()
                };
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductId = x.Id, Quantity = productQuantities.Count(productId => productId == x.Id) }));

                var rowsEffected = _shopService.SaveOrder(newOrder);

                _result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                _result.Data = new { Success = false };
            }
            return _result;
        }
    }
}
