using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;
using SkateWebApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SkateAppWeb.Models.Products
{
    public class SkateboardCreateVM
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
        public string? WheelId { get; set; }
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
        public string DeckId { get; set; } = null!;
        public Deck? Deck { get; set; } 

        public decimal Price { get; set; }

        [Range(1,3)]
        public int Quantity { get; set; }
    }
}
