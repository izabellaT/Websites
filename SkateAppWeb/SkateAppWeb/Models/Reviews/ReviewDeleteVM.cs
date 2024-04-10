namespace SkateAppWeb.Models.Reviews
{
	public class ReviewDeleteVM
	{
		public string Id { get; set; } = null!;
		public string ProductId { get; set; } = null!;
		public string Product { get; set; } = null!;

		public string UserId { get; set; } = null!;
		public string User { get; set; } = null!;

		public string Content { get; set; } = null!;
		public int Rating { get; set; }
		public string Posted { get; set; } = null!;
        public string Picture { get; set; } = null!;
    }
}
