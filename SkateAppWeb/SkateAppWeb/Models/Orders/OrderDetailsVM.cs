namespace SkateAppWeb.Models.Orders
{
    public class OrderDetailsVM
    {
        public string Id { get; set; } = null!;
        public string OrderDate { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public string User { get; set; } = null!;

        public string ProductId { get; set; } = null!;
        public string Product { get; set; } = null!;
        public string Picture { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        //public int Count { get; set; }
        public string? Promocode { get; set; }
    }
}
