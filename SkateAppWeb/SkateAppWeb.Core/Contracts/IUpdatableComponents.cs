using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
     public interface IUpdatableComponents
    {
        public bool UpdateBearing(string id, string name, int brandId, int categoryId, string color, int count, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool UpdateDeck(string id, string name, int brandId, int categoryId, string color, string shape, double size, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool UpdateGriptape(string id, string name, int brandId, int categoryId, string color, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool UpdateTruck(string id, string name, int brandId, int categoryId, string color, double size, int count, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool UpdateWheel(string id, string name, int brandId, int categoryId, string color, int count, double size, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
    }
}
