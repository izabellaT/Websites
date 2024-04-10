using SkateWebApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Blogs
{
	public class BlogCreateVM
	{
		public string? Id { get; set; }

		public string? UserId { get; set; }
		public string? User { get; set; }

        public string? Content { get; set; }
        public string? VideoLink { get; set; }
        public string? Picture { get; set; }
        public DateTime Posted { get; set; }
        
    }
}
