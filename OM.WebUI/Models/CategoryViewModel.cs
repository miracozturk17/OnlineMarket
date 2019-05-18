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
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [MinLength(3), MaxLength(500)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }
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