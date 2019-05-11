using System.Collections.Generic;

namespace OM.Entities.EntityClass
{
    public class Category : BaseEntity
    {
        public string ImageUrl{ get; set; }

        public bool IsFeatured { get; set; }

        public List<Product> Products{ get; set; }
    }
}
 
