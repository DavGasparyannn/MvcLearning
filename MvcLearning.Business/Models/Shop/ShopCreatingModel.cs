using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MvcLearning.Business.Models.Shop
{
    public class ShopCreatingModel
    {
        [Required(ErrorMessage = "Shop name is required.")]
        [StringLength(100, ErrorMessage = "Shop name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "shop_default.png";
    }
}