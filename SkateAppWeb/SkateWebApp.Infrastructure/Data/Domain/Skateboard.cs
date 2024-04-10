using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Domain
{
    public class Skateboard : Product
    {
        public string DeckId { get; set; } = null!;
        public virtual Deck Deck { get; set; } = null!;
        public string TruckId { get; set; } = null!;
        public virtual Truck Truck { get; set; } = null!;
        public string WheelId { get; set; } = null!;
        public virtual Wheel Wheel { get; set; } = null!;
        public string BearingId { get; set; } = null!;
        public virtual Bearing Bearing { get; set; } = null!;
        public string GriptapeId { get; set; } = null!;
        public virtual Griptape Griptape { get; set; } = null!;
        public string AccessoryId { get; set; } = null!;
        public virtual Accessory Accessory { get; set; } = null!;
        public string Shape { get; set; } = null!;
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                 $"Deck: {this.Deck.Shape} {this.Deck.Material}",
                 $"Trucks: set of {this.Truck.Count} {this.Truck.Material} {this.Truck.Size}",
                 $"Wheels: set of {this.Wheel.Count} {this.Wheel.Material} {this.Wheel.Size}",
                 $"Bearings: {this.Bearing.Material} {this.Bearing.Count} {this.Bearing.Color}",
                 $"Griptape: {this.Griptape.Color}"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                 $"Griptape: {this.Griptape.Color}",
                 $"Wheels: set of {this.Wheel.Count} {this.Wheel.Material} {this.Wheel.Size}",
                 $"Deck: {this.Deck.Shape} {this.Deck.Material}",
            };
    }
}
