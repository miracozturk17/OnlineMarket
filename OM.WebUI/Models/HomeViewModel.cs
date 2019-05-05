using System.Collections.Generic;
using OM.Entities.EntityClass;

namespace OM.WebUI.Models
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }

        public List<Product> Products { get; set; }
    }
}