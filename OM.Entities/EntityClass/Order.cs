using System;
using System.Collections.Generic;

namespace OM.Entities.EntityClass
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }

        public DateTime OrderedAt { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
