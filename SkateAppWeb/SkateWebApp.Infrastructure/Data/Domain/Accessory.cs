using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class Accessory : Product
    {
        public string Description { get; set; } = null!;
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Material: {this.Material}",
                $"Description: {Description}"
            };
        public override IEnumerable<string> PartialDescription
             => new List<string>()
             {
                 $"Material: {this.Material}"
             };
    }
}
