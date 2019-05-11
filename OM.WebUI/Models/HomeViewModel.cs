using System.Collections.Generic;
using OM.Entities.EntityClass;

namespace OM.WebUI.Models
{
    public class HomeViewModel
    {
        public List<Category> FeaturedCategories { get; set; }

        public List<Product> FeaturedProducts { get; set; }
    }
}