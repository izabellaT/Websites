using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Brands
{
    public class BrandCreateVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = null!;
       
    }
}
