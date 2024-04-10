using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Brands
{
    public class BrandPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Brand")]
        public string Name { get; set; } = null!;
    }
}
