using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Infrastructure.Data;
using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SkateAppWeb.Core.Services
{
    public class AccessoryService : IAccessoryService
    {
        private readonly ApplicationDbContext _context;

        public AccessoryService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool CreateAccessory(string name, int brandId, int categoryId, string color, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var accessory = new Accessory()
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                CategoryId = 3,
                Color = color,
                Price = price,
                Quantity = quantity,
                Material = material,
                Description = description,
                Discount = discount,
                Picture = picture
            };

            _context.Accessories.Add(accessory);

            return _context.SaveChanges() != 0;
        }

        public List<Product> GetAccessories()
        {
            return _context.Products.Where(x => x.Category.CategoryName == "Accessories").ToList();
        }

        public bool UpdateAccessory(string id, string name, int brandId, int categoryId, string color, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var item = _context.Accessories.Find(id);

            if (item == null)
            {
                return false;
            }
            item.ProductName = name;
            item.Brand = _context.Brands.Find(brandId);
            item.Category = _context.Categories.Find(categoryId);
            item.Color = color;
            item.Price = price;
            item.Quantity = quantity;
            item.Material = material;
            item.Description = description;
            item.Discount = discount;
            item.Picture = picture;

            _context.Update(item);

            return _context.SaveChanges() != 0;
        }
    }
}
