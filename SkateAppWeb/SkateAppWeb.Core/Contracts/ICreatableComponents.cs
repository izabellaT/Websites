using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface ICreatableComponents
    {
        public bool CreateBearing(string name, int brandId, int categoryId, string color, int count, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool CreateDeck(string name, int brandId, int categoryId, string color, string shape, double size, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool CreateGriptape(string name, int brandId, int categoryId, string color, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool CreateTruck(string name, int brandId, int categoryId, string color, double size, int count, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
        public bool CreateWheel(string name, int brandId, int categoryId, string color, int count, double size, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
    }
}
