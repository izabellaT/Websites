using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SkateAppWeb.Models.Brands
{
    public class BrandEditVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = null!;
    }
}
