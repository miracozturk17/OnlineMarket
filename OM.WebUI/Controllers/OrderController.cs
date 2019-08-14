using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OM.Services.Services;
using OM.WebUI.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace OM.WebUI.Controllers
{
    public class OrderController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string userId, string status, int? pageNo)
        {
            OrdersViewModel model = new OrdersViewModel {UserId = userId, Status = status};

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = ConfigrationService.Instance.PageSize();

            model.Orders = OrdersService.Instance.SearchOrders(userId, status, pageNo.Value, pageSize);
            int totalRecords = OrdersService.Instance.SearchOrdersCount(userId, status);

            model.Pager = new Pager(totalRecords,pageNo,pageSize);
            
            return View(model);
        }

        public ActionResult Details(int id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel {Order = OrdersService.Instance.GetOrderByID(id)};

            if (model.Order != null)
            {
                model.OrderBy = UserManager.FindById(model.Order.UserId);
            }

            model.AvailableStatuses = new List<string>() { "Pending", "In Progress", "Delivered" };

            return View(model);
        }

        public JsonResult ChangeStatus(string status, int ID)
        {
            JsonResult result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new {Success = OrdersService.Instance.UpdateOrderStatus(ID, status)}
            };
            return result;
        }
    }
}