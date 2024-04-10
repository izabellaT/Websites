using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface IOrderService
    {
        public bool Create(string productId, string userId, int quantity, decimal price, string promocode);
        public List<Order> GetOrders();
        public List<Order> GetOrdersByUser(string userId);
        public Order GetOrder(string orderId);
        public bool RemoveById(string orderId);
        public bool Update(string id, string productId, string userId, int quantity, decimal price, string promocode);
    }
}
