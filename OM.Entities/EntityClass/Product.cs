namespace OM.Entities.EntityClass
{
    public class Product : BaseEntity
    {
        public decimal UnitePrice { get; set; }

        public Category Category { get; set; }
    }
}
