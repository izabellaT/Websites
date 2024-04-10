using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
	public class Review
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
		public string Id { get; set; } = null!;

		[StringLength(500, MinimumLength = 2, ErrorMessage = "The minimal length is 2 characters, maximum length is 500 characters")]
		public string Content { get; set; } = null!;

		public string ProductId { get; set; } = null!;
		public virtual Product Product { get; set; } = null!;

		public string UserId { get; set; } = null!;
		public virtual ApplicationUser User { get; set; } = null!;

		[Range(1,5, ErrorMessage = "The rating must be positive number between 1 and 5")]
        public int Rating { get; set; }
        public DateTime Posted { get; set; }
    }
}
