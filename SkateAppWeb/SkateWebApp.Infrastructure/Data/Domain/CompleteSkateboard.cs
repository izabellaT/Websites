using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class CompleteSkateboard : Product
    {
        public string Description { get; set; } = null!;
        public string TruckId { get; set; } = null!;
        public virtual Truck Truck { get; set; } = null!;
        public string WheelId { get; set; } = null!;        
        public virtual Wheel Wheel { get; set; } = null!;
        public string BearingId { get; set; } = null!;
        public virtual Bearing Bearing { get; set; } = null!;
        public string GriptapeId { get; set; } = null!;
        public virtual Griptape Griptape { get; set; } = null!;
        public string Shape { get; set; } = null!;
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Material: {this.Material}",
                $"Shape: {this.Shape}",
                $"Size: {this.Size}",
                $"Color: {this.Color}",
                $"Other: {Description}"
            };
        public override IEnumerable<string> PartialDescription
             => new List<string>()
             {
                $"Shape: {this.Shape}",
                $"Color: {this.Color}"
             };
    }
}
