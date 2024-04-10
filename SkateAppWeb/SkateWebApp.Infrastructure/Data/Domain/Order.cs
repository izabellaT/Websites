using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        
        [Required]
        public string ProductId { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
       public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Promocode { get; set; }
    }
}
