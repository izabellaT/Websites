namespace SkateAppWeb.Models.Favorites
{
    public class FavoriteListingVM
    {
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public string User { get; set; } = null!;

        public string ProductId { get; set; } = null!;
        public string Product { get; set; } = null!;
        public string Picture { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
    }
}
