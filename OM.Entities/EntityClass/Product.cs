namespace OM.Entities.EntityClass
{
    public sealed class Product : BaseEntity
    {
        public decimal UnitePrice { get; set; }

        //public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
