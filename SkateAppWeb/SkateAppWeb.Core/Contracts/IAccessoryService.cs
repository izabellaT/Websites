using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface IAccessoryService
    {
        public List<Product> GetAccessories();
        public bool CreateAccessory(string name, int brandId, int categoryId, string color, decimal price, 
            int quantity, string material, string description, decimal discount, string picture);
        public bool UpdateAccessory(string id, string name, int brandId, int categoryId, string color, decimal price,
            int quantity, string material, string description, decimal discount, string picture);
    }
}
