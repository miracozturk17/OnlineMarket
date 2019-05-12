using System.Collections.Generic;
using OM.Entities.EntityClass;

namespace OM.WebUI.Models
{
    public class ShopViewModel
    {
        public List<Product> CartProducts { get; set; }

        public List<int> CartProductIDs { get; set; }
    }
}