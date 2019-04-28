using System.Collections.Generic;

namespace OM.Entities.EntityClass
{
    public class Category : BaseEntity
    {
        public List<Product> Products{ get; set; }
    }
}
