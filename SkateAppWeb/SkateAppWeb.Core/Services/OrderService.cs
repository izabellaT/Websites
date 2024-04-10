using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Infrastructure.Data;
using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Create(string productId,string userId, int quantity, decimal price, string promocode)
        {  
            var product = _context.Products.SingleOrDefault(p => p.Id == productId);
            var user = _context.Users.SingleOrDefault(s => s.Id == userId);

            Order order = new Order
            {
                OrderDate = DateTime.Now,
                ProductId = productId,
                Quantity = quantity,
                Price = product.Price,
                UserId = userId,
                Discount = product.Discount,
                Promocode = promocode
            };

            product.Quantity -= quantity;

            if (promocode!=null)
            {
                if(promocode.Contains("Bella15"))
            {
                order.Discount = order.Discount + 15;
            }
            }

            _context.Products.Update(product);
            _context.Orders.Add(order);

            return _context.SaveChanges() != 0;
        }

        public Order GetOrder(string orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(x => x.OrderDate).ToList();
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders.Where(x => x.UserId == userId).OrderByDescending(x => x.OrderDate).ToList();
        }

        public bool Update(string id, string productId, string userId, int quantity, decimal price, string promocode)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == productId);
            var item = _context.Orders.Find(id);

            if (item == null)
            {
                return false;
            }

            item.OrderDate = DateTime.Now;
            item.Price = price;
            item.Quantity = quantity;
            item.Promocode = promocode;

            _context.Orders.Update(item);
            return _context.SaveChanges() !=0;
        }

        public bool RemoveById(string orderId)
        {
            var item = GetOrder(orderId);

            if (item == default(Order))
            {
                return false;
            }
            _context.Orders.Remove(item);

            return _context.SaveChanges() != 0;
        }
    }
}
