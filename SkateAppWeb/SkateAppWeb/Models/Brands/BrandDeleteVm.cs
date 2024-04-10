using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SkateAppWeb.Models.Brands
{
    public class BrandDeleteVm
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = null!;
    }
}
