using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebAppStaj.Domain
{
    public class Movie
    {
        public Movie()
        {
            this.MoviesActor = new HashSet<MovieActor>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [ForeignKey("Genre")]
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ICollection<MovieActor> MoviesActor { get; set; }
    }
}
