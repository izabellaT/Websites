using System.ComponentModel.DataAnnotations;
using WebShopApp.Models.Category;

namespace WebShopApp.Models.Order
{
    public class OrderCreateVM
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int QuantityinStock { get; set; }
        public string? Picture { get; set; }

        [Range(0,1000)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
