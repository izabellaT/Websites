using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;
using SkateWebApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Products
{
    public class CompleteSkateboardCreateVM
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
        public string WheelId { get; set; } = null!;
        public Wheel? Wheel { get; set; }

        [Required]
        public string TruckId { get; set; } = null!;
        public Truck? Truck { get; set; }

        [Required]
        public string BearingId { get; set; } = null!;
        public Bearing? Bearing { get; set; }

        [Required]
        public string GriptapeId { get; set; } = null!;
        public Griptape? Griptape { get; set; }

        [Required]
        public string Color { get; set; } = null!;

        [Required]
        public double Size { get; set; } 

        [Required]
        public string Shape { get; set; } = null!;

        [Required]
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
