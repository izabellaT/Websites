using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class Deck : Product
    {
        public string Description { get; set; } = null!;
        public string Shape { get; set; } = null!;
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Material: {this.Material}",
                $"Shape: {this.Shape}",
                $"Other: {Description}"
            };
        public override IEnumerable<string> PartialDescription
             => new List<string>()
             {
                $"Shape: {this.Shape}",
                $"MAterial: {this.Material}"
             };
    }
}
