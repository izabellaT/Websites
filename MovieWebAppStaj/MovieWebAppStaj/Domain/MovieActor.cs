using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebAppStaj.Domain
{
    public class MovieActor
    {
        public int Id { get; set; }

        [ForeignKey("Movie")]
        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("Actor")]
        [Required]
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
