using SkateAppWeb.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Orders
{
    public class OrderCreateVM
    {
        public string? Id { get; set; } 
        public DateTime OrderDate { get; set; }
        public string? ProductId { get; set; } 
        public string? ProductName { get; set; } 

        public string? UserId { get; set; }
        public string? User { get; set; } 

        public int QuantityInStock { get; set; }
        public int Count { get; set; }
        public string? Picture { get; set; }

        [Range(0, 1000)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Promocode { get; set; }
    }
}
