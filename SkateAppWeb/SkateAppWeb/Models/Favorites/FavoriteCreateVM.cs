using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Favorites
{
    public class FavoriteCreateVM
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }

        public string? UserId { get; set; }
        public string? User { get; set; }

        public int QuantityInStock { get; set; }
        public string? Picture { get; set; }

        [Range(0, 1000)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
    }
}
