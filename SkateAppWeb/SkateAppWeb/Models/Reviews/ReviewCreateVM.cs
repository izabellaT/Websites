using SkateWebApp.Infrastructure.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Reviews
{
	public class ReviewCreateVM
	{
		public string? Id { get; set; }
		public string? ProductId { get; set; }
		public string? Product { get; set; }

		public string? UserId { get; set; }
		public string? User { get; set; }

        public string? Content { get; set; }
		[Range(0,5,ErrorMessage = "The maximum rating is 5 stars")]
        public int Rating { get; set; }
		public DateTime Posted { get; set; }
        public string? Picture { get; set; }
    }
}
