using SkateWebApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Products
{
    public class ProductAllTableVM
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; } = null!;
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Picture { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
