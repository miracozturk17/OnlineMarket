using System.Collections.Generic;
using OM.Entities.EntityClass;

namespace OM.WebUI.Models
{
    public class ProductSearchViewModel
    {
        public List<Product> Products { get; set; }

        public string SearchTerm { get; set; }

        public BaseListingViewmodel Pager { get; set; }
    }

    public class NewProductViewModel
    {
        public NewProductViewModel(string name, string description, decimal price, int categoryId, string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            ImageUrl = imageUrl;
        }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int CategoryId { get; }

        public string ImageUrl { get; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class ProductViewModel
    {
        public Product Product { get; set; }
    }
}