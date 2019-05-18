using System.ComponentModel.DataAnnotations;

namespace OM.Entities.EntityClass
{
    public sealed class Product : BaseEntity
    {
        [Range(1, 100000)]
        public decimal UnitePrice { get; set; }

        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public Category Category { get; set; }
    }
}
