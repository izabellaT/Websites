using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Infrastructure.Data;
using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SkateAppWeb.Core.Services
{
    public class CompleteSkateboardService : ICompleteSkateboardService
    {
        private readonly ApplicationDbContext _context;

        public CompleteSkateboardService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool CreateCompleteSkateboard(string name, int brandId, int categoryId, string wheelId, string bearingId, string griptapeId, string truckId, string color, string shape, double size, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var completeSkateboard = new CompleteSkateboard()
            {
                WheelId = wheelId,
                BearingId = bearingId,
                GriptapeId = griptapeId,
                TruckId = truckId,
                Brand = _context.Brands.Find(brandId),
                CategoryId = 1,
                Quantity = quantity,
                ProductName = name,
                Color = color,
                Shape = shape,
                Size = size,
                Material = material,
                Discount = discount,
                Picture = picture,
                Price = 0,
                Wheel = _context.Wheels.Find(wheelId),
                Bearing = _context.Bearings.Find(bearingId),
                Griptape = _context.Griptapes.Find(griptapeId),
                Truck = _context.Trucks.Find(truckId)
            };

            completeSkateboard.Price = completeSkateboard.Wheel.Price +
                completeSkateboard.Bearing.Price + completeSkateboard.Griptape.Price +
                completeSkateboard.Truck.Price - (completeSkateboard.Wheel.Price +
                completeSkateboard.Bearing.Price + completeSkateboard.Griptape.Price +
                completeSkateboard.Truck.Price)*discount/100;
            _context.CompleteSkateboards.Add(completeSkateboard);

            Product[] components =
            {
                _context.Wheels.Find(wheelId),
                _context.Bearings.Find(bearingId),
                _context.Griptapes.Find(griptapeId),
                _context.Trucks.Find(truckId)
            };

            foreach (var item in components)
            {
                item.Quantity -= quantity;
                _context.Update(item);
            }
            return _context.SaveChanges() != 0;
        }

        public bool UpdateCompleteSkateboard(string id, string name, int brandId, int categoryId, string wheelId, string bearingId, string griptapeId, string truckId, string color, string shape, double size, decimal price, int quantity, string material, string description, decimal discount, string picture)
        {
            var completeSkateboard = _context.CompleteSkateboards.Find(id);

            if (completeSkateboard == null)
            {
                return false;
            }

            var newQuantity = quantity - completeSkateboard.Quantity;

            completeSkateboard.WheelId = wheelId;
            completeSkateboard.BearingId = bearingId;
            completeSkateboard.GriptapeId = griptapeId;
            completeSkateboard.TruckId = truckId;
            completeSkateboard.Brand = _context.Brands.Find(brandId);
            completeSkateboard.Category = _context.Categories.Find(categoryId);
            completeSkateboard.Quantity = quantity;
            completeSkateboard.ProductName = name;
            completeSkateboard.Color = color;
            completeSkateboard.Shape = shape;
            completeSkateboard.Size = size;
            completeSkateboard.Material = material;
            completeSkateboard.Discount = discount;
            completeSkateboard.Picture = picture;

            Product[] components =
            {
                _context.Wheels.Find(wheelId),
                _context.Bearings.Find(bearingId),
                _context.Griptapes.Find(griptapeId),
                _context.Trucks.Find(truckId)
            };

            foreach (var item in components)
            {
                item.Quantity -= newQuantity;
                _context.Update(item);
            }


            _context.CompleteSkateboards.Update(completeSkateboard);
            return _context.SaveChanges() != 0;
        }
    }
}
