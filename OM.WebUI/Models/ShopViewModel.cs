using System.Collections.Generic;
using OM.Entities.EntityClass;

namespace OM.WebUI.Models
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }

        public List<int> CartProductIDs { get; set; }

        public ApplicationUser User { get; set; }
    }

    public abstract class ShopViewModel
    {
        public int MaximumPrice { get; set; }

        public List<Category> FeaturedCategories { get; set; }

        public List<Product> Products { get; set; }

        public int? SortBy { get; set; }

        public int? CategoryId { get; set; }

        public BaseListingViewmodel Pager { get; set; }

        public string SearchTerm { get; set; }
    }

    public abstract class FilterProductsViewModel
    {
        public List<Product> Products { get; set; }

        public BaseListingViewmodel Pager { get; set; }

        public int? SortBy { get; set; }

        public int? CategoryId { get; set; }

        public string SearchTerm { get; set; }
    }
}