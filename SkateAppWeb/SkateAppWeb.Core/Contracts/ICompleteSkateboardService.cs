using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface ICompleteSkateboardService
    {
        public bool CreateCompleteSkateboard(string name, int brandId, int categoryId, string wheelId, string bearingId, string griptapeId, string truckId, string color,
            string shape, double size, decimal price, int quantity, string material, string description, decimal discount, string picture);
        public bool UpdateCompleteSkateboard(string id, string name, int brandId, int categoryId, string wheelId, string bearingId, string griptapeId, string truckId, string color,
            string shape, double size, decimal price, int quantity, string material, string description, decimal discount, string picture);
    }
}
