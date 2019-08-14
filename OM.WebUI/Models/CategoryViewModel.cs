using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OM.Entities.EntityClass;

namespace OM.WebUI.Models
{
    public class CategoryViewModel
    {

        public List<Category> Categories { get; set; }

        public string SearchTerm { get; set; }

        public BaseListingViewmodel Pager { get; set; }
    }

    public class NewCategoryViewModel
    {
        public NewCategoryViewModel()
        {
        }

        public NewCategoryViewModel(string description, string imageUrl, bool isFeatured)
        {
            Description = description;
            ImageUrl = imageUrl;
            IsFeatured = isFeatured;
        }

        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [MinLength(3), MaxLength(500)]
        public string Description { get; }

        public string ImageUrl { get; }

        public bool IsFeatured { get; }
    }

    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [MinLength(3), MaxLength(500)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }
    }
}