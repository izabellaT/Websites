using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;
using SkateWebApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SkateAppWeb.Models.Products
{
    public class AccessoryCreateVM
    {
        public string? Id { get; set; } 

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int BrandId { get; set; }
        public virtual List<BrandPairVM> Brands { get; set; } = new List<BrandPairVM>();

        [Required]
        public int CategoryId { get; set; }
        public virtual List<CategoryPairVM> Categories { get; set; } = new List<CategoryPairVM>();

        [Required]
        public string Color { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        [Required]
        public string Material { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public string Picture { get; set; } = null!;

    }
}
