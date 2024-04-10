using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class Blog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public string Id { get; set; } = null!;
        public string Content { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public string? Picture { get; set; }
        public string? VideoLink { get; set; }
        public DateTime Posted { get; set; }
    }
}
