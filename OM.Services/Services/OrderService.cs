using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public class OrdersService
    {
        //DEPENDANCY INJECTIONA AYKIRI ESTETIK OLMAYAN KULLANIM.

        private readonly OMContext _context;

        #region SINGLETON PATTERN
        public static OrdersService Instance
        {
            get
            {
                if (InstanceValid == null) InstanceValid = new OrdersService();

                return InstanceValid;
            }
        }
        private static OrdersService InstanceValid { get; set; }

        private OrdersService()
        {
        }
        #endregion

        public List<Order> SearchOrders(string userId, string status, int pageNo, int pageSize)
        {
            using (var context = new OMContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int SearchOrdersCount(string userId, string status)
        {
            using (var context = new OMContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId.ToLower().Contains(userId.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return orders.Count;
            }
        }


        public Order GetOrderByID(int ID)
        {
            using (var context = new OMContext())
            {
                return context.Orders.Where(x => x.OrderId == ID).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
            }
        }

        public bool UpdateOrderStatus(int ID, string status)
        {
            using (var context = new OMContext())
            {
                var order = context.Orders.Find(ID);

                order.Status = status;

                context.Entry(order).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }
    }
}