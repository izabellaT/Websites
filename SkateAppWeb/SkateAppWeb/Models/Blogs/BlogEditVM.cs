namespace SkateAppWeb.Models.Blogs
{
	public class BlogEditVM
	{
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string User { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? VideoLink { get; set; }
        public string Picture { get; set; } = null!;
        public DateTime Posted { get; set; } 
    }
}
