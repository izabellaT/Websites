using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Infrastructure.Data;
using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SkateAppWeb.Core.Services
{
    public class SkateboardService : ISkateboardService
    {
        private readonly ApplicationDbContext _context;

        public SkateboardService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool CreateSkateboard(string name, int brandId, int categoryId, string wheelId, string deckId, string bearingId, string griptapeId, string truckId, int quantity)
        {
            var skateboard = new Skateboard
            {
                WheelId = wheelId,
                DeckId = deckId,
                BearingId = bearingId,
                GriptapeId = griptapeId,
                TruckId = truckId,
                BrandId = 1,
                CategoryId = 9,
                Quantity = quantity,
                ProductName = name,
                Color = "multicolor",
                Material = "multiMaterial",
                Picture = _context.Decks.Find(deckId).Picture,
                Wheel = _context.Wheels.Find(wheelId),
                Deck = _context.Decks.Find(deckId),
                Bearing = _context.Bearings.Find(bearingId),
                Griptape = _context.Griptapes.Find(griptapeId),
                Truck = _context.Trucks.Find(truckId),
            };

            skateboard.Price = skateboard.Wheel.Price + skateboard.Deck.Price +
            skateboard.Bearing.Price + skateboard.Griptape.Price +
            skateboard.Truck.Price;
            _context.Skateboards.Add(skateboard);

            Product[] components =
            {
                _context.Decks.Find(deckId),
                _context.Wheels.Find(wheelId),
                _context.Bearings.Find(bearingId),
                _context.Griptapes.Find(griptapeId),
                _context.Trucks.Find(truckId),
            };

            foreach (var item in components)
            {
                item.Quantity -= quantity;
            }
            return _context.SaveChanges() != 0;

        }

        public bool UpdateSkateboard(string id, string name, int brandId, int categoryId, string wheelId, string deckId, string bearingId, string griptapeId, string truckId, int quantity)
        {
            var skateboard = _context.Skateboards.Find(id);

            if (skateboard == null)
            {
                return false;
            }

            var newQuantity = quantity - skateboard.Quantity;

            skateboard.WheelId = wheelId;
            skateboard.DeckId = deckId;
            skateboard.BearingId = bearingId;
            skateboard.GriptapeId = griptapeId;
            skateboard.TruckId = truckId;
            skateboard.Brand = _context.Brands.Find(brandId);
            skateboard.Category = _context.Categories.Find(categoryId);
            skateboard.Quantity = quantity;
            skateboard.ProductName = name;


            Product[] components =
            {
               _context.Decks.Find(deckId),
                _context.Wheels.Find(wheelId),
                _context.Bearings.Find(bearingId),
                _context.Griptapes.Find(griptapeId),
                _context.Trucks.Find(truckId),
            };

            foreach (var item in components)
            {
                item.Quantity -= newQuantity;
                _context.Update(item);
            }


            _context.Skateboards.Update(skateboard);
            return _context.SaveChanges() != 0;
        }
    }
}
