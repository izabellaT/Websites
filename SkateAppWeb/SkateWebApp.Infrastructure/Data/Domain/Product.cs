using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string ProductName { get; set; } = null!;

        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public double Size { get; set; }
        public string Material { get; set; } = null!;
        public string Color { get; set; } = null!;

        [Range(0, 5000)]
        public int Quantity { get; set; }

        [Range(0, 600)]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        [NotMapped()]
        public virtual IEnumerable<string> FullDescription { get; } = null!;
        [NotMapped()]
        public virtual IEnumerable<string> PartialDescription { get; } = null!;
        public virtual ICollection<Order> Orders { get; set; } = null!;
        public virtual ICollection<Favorite> Favorites { get; set; } = null!;
		public virtual ICollection<Review> Reviews { get; set; } = null!;
	}
}
