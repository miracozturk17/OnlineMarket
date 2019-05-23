using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public class ShopService
    {
        private readonly OMContext _context;
        private static ShopService Instance { get; set; }

        public ShopService(OMContext context)
        {
            _context = context;
        }

        public int SaveOrder(Order order)
        {
            _context.Orders.Add(order);
            return _context.SaveChanges();
        }
    }
}