using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class Bearing : Product
    {
        public string Description { get; set; } = null!;
        public int Count { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Material: {this.Material}",
                $"Color: {this.Color}",
                $"Set of: {this.Count}",
                $"Other: {Description}"
            };
        public override IEnumerable<string> PartialDescription
             => new List<string>()
             {
                $"Material: {this.Material}"
             };
    }
}
