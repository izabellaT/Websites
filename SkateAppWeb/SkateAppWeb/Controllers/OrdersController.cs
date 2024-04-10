using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.CodeAnalysis;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Orders;
using SkateAppWeb.Models.Products;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Globalization;
using System.Security.Claims;

namespace SkateAppWeb.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrdersController(IOrderService orderService, IProductService productService)
        {
            this._orderService = orderService;
            _productService = productService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All()
        {
            List<OrderListingVM> orders = _orderService.GetOrders()
                .Select(x => new OrderListingVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Product.Price,
                    Discount = x.Product.Discount,
                    TotalPrice = x.Quantity * x.Price - x.Quantity * x.Price * x.Discount / 100
                }).ToList();

            return View(orders);
        }

        public IActionResult My()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderListingVM> orders = _orderService.GetOrdersByUser(userId)
                .Select(x => new OrderListingVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Product.Price,
                    Discount = x.Product.Discount,
                    TotalPrice = x.Quantity * x.Price - x.Quantity * x.Price * x.Discount / 100,
                    Promocode = null
                }).ToList();

            return View("All", orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderCreateVM viewModel)
        {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var product = this._productService.GetProductById(viewModel.ProductId);

               if (userId == null || product == null || product.Quantity < viewModel.Quantity)
               {
                return RedirectToAction("Denied");
               }

                var created = _orderService.Create(viewModel.ProductId, userId, viewModel.Quantity, viewModel.Price, viewModel.Promocode);
               
            if (created)
                { 
                if (viewModel.Promocode != null)
                {
                if (viewModel.Promocode != "Bella15")
                {
                    return RedirectToAction("WrongPromocode");
                }
            } 
                    return RedirectToAction("My");
            }

              return RedirectToAction();
        }

        public IActionResult Denied()
        {
            return View();
        }
        public IActionResult WrongPromocode()
        {
            return View();
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var order = _orderService.GetOrder(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var detailsViewModel = new OrderDetailsVM()
            {
                Id = order.Id,
                OrderDate = order.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                UserId = order.UserId,
                User = order.User.UserName,
                ProductId = order.ProductId,
                Product = order.Product.ProductName,
                Picture = order.Product.Picture,
                Quantity = order.Quantity,
                Price = order.Product.Price,
                Discount = order.Product.Discount,
                TotalPrice = order.Quantity * order.Price - order.Quantity * order.Price * order.Discount / 100
            };

            return View(detailsViewModel);
        }
        public ActionResult Delete(string id)
        {
            Order item = _orderService.GetOrder(id);
            if (item == null)
            {
                return NotFound();
            }
            OrderDeleteVM order = new OrderDeleteVM()
            {
                Id = item.Id,
                OrderDate = item.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Quantity = item.Quantity,
                Price = item.Product.Price,
                Picture = item.Product.Picture,
                TotalPrice = item.Quantity * item.Price - item.Quantity * item.Price * item.Discount / 100,
                User = item.User.UserName,
				Discount = item.Discount
            };
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var isDeleted = _orderService.RemoveById(id);
                if (isDeleted)
                {
                    return this.RedirectToAction("My");
                }
                return this.RedirectToAction("My");
            }
            catch
            {
                return this.RedirectToAction("My");
            }
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string id)
        {
            var order = _orderService.GetOrder(id);

            var viewModel = new OrderCreateVM()
            {
                Id = order.Id,
                UserId = order.UserId,
                User = order.User.UserName,
                ProductId = order.ProductId,
                Picture = order.Product.Picture,
                Quantity = order.Quantity,
                Price = order.Product.Price,
                Discount = order.Product.Discount,
                TotalPrice = order.Quantity * order.Price - order.Quantity * order.Price * order.Discount / 100,
                Promocode = null
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, OrderCreateVM editViewModel)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _orderService.Update(editViewModel.Id, editViewModel.ProductId,editViewModel.UserId, editViewModel.Quantity, editViewModel.Price, editViewModel.Promocode);

                if (isUpdated)
                {
                    return RedirectToAction("My", "Orders");
                }
            }
            return View();
        }
        
    }
}
