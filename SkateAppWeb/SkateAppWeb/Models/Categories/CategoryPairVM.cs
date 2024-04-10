using System.ComponentModel.DataAnnotations;

namespace SkateAppWeb.Models.Categories
{
    public class CategoryPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; } = null!;
    }
}
