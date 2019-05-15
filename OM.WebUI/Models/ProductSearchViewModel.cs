using System.Collections.Generic;
using OM.Entities.EntityClass;

namespace OM.WebUI.Models
{
    public class ProductSearchViewModel
    {
        public int PageNo{ get; set; }

        public List<Product> Products{ get; set; }

        public string Searchterm { get; set; }
    }
}