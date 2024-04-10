using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Infrastructure.Data;
using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipelines;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SkateAppWeb.Core.Services
{
    public class ComponentsService : IComponentService
    {
        private readonly ApplicationDbContext _context;

        public ComponentsService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool CreateBearing(string name, int brandId, int categoryId, string color, int count, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
           
            var bearing = new Bearing
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                CategoryId = 8,
                Color = color,
                Count = count,
                Price = price,
                Quantity = quantity,
                Material = material,
                Description = description,
                Discount = discount,
                Picture = picture
            };

            _context.Bearings.Add(bearing);

            return _context.SaveChanges() != 0;
        }

        public bool CreateDeck(string name, int brandId, int categoryId, string color, string shape, double size, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var deck = new Deck
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                CategoryId = 5,
                Color = color,
                Shape = shape,
                Size = size,
                Price = price,
                Quantity = quantity,
                Material = material,
                Description = description,
                Discount = discount,
                Picture = picture
            };

            _context.Decks.Add(deck);

            return _context.SaveChanges() != 0;
        }

        public bool CreateGriptape(string name, int brandId, int categoryId, string color, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var griptape = new Griptape
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                CategoryId = 6,
                Color = color,
                Price = price,
                Quantity = quantity,
                Material = material,
                Description = description,
                Discount = discount,
                Picture = picture
            };

            _context.Griptapes.Add(griptape);

            return _context.SaveChanges() != 0;
        }

        public bool CreateTruck(string name, int brandId, int categoryId, string color, double size, int count, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var truck = new Truck
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                CategoryId = 4,
                Color = color,
                Size = size,
                Count = count,
                Price = price,
                Quantity = quantity,
                Material = material,
                Description = description,
                Discount = discount,
                Picture = picture
            };

            _context.Trucks.Add(truck);

            return _context.SaveChanges() != 0;
        }

        public bool CreateWheel(string name, int brandId, int categoryId, string color, int count, double size, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var wheel = new Wheel
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                CategoryId = 2,
                Color = color,
                Count = count,
                Size = size,
                Price = price,
                Quantity = quantity,
                Material = material,
                Description = description,
                Discount = discount,
                Picture = picture
            };

            _context.Wheels.Add(wheel);

            return _context.SaveChanges() != 0;
        }

        public List<Product> GetComponents()
        {
            return _context.Products.Where(x => x.Category.CategoryName != "Complete skateboard"
            && x.Category.CategoryName != "Skateboard" && x.Category.CategoryName != "Accessories").ToList();
        }

        public bool UpdateDeck(string id, string name, int brandId, int categoryId, string color, string shape, double size, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var item = _context.Decks.Find(id);

            if (item == null)
            {
                return false;
            }
            item.ProductName = name;
            item.Brand = _context.Brands.Find(brandId);
            item.Category = _context.Categories.Find(categoryId);
            item.Color = color;
            item.Shape = shape;
            item.Size = size;
            item.Price = price;
            item.Quantity = quantity;
            item.Material = material;
            item.Description = description;
            item.Discount = discount;
            item.Picture = picture;

            _context.Update(item);

            return _context.SaveChanges() != 0;
        }

        public bool UpdateGriptape(string id, string name, int brandId, int categoryId, string color, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var item = _context.Griptapes.Find(id);

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

        public bool UpdateTruck(string id, string name, int brandId, int categoryId, string color, double size, int count, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var item = _context.Trucks.Find(id);

            if (item == null)
            {
                return false;
            }
            item.ProductName = name;
            item.Brand = _context.Brands.Find(brandId);
            item.Category = _context.Categories.Find(categoryId);
            item.Color = color;
            item.Size = size;
            item.Count = count;
            item.Price = price;
            item.Quantity = quantity;
            item.Material = material;
            item.Description = description;
            item.Discount = discount;
            item.Picture = picture;

            _context.Update(item);

            return _context.SaveChanges() != 0;
        }

        public bool UpdateWheel(string id, string name, int brandId, int categoryId, string color, int count, double size, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var item = _context.Wheels.Find(id);

            if (item == null)
            {
                return false;
            }
            item.ProductName = name;
            item.Brand = _context.Brands.Find(brandId);
            item.Category = _context.Categories.Find(categoryId);
            item.Color = color;
            item.Count = count;
            item.Size = size;
            item.Price = price;
            item.Quantity = quantity;
            item.Material = material;
            item.Description = description;
            item.Discount = discount;
            item.Picture = picture;

            _context.Update(item);

            return _context.SaveChanges() != 0;
        }

        public bool UpdateBearing(string id, string name, int brandId, int categoryId, string color, int count, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var item = _context.Bearings.Find(id);

            if (item == null)
            {
                return false;
            }
            item.ProductName = name;
            item.Brand = _context.Brands.Find(brandId);
            item.Category = _context.Categories.Find(categoryId);
            item.Color = color;
            item.Count = count;
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
