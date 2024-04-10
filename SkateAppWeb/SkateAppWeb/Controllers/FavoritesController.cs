using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Favorites;
using SkateAppWeb.Models.Orders;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Globalization;
using System.Security.Claims;

namespace SkateAppWeb.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IProductService _productService;

        public FavoritesController(IFavoriteService favoriteService, IProductService productService)
        {
            this._favoriteService = favoriteService;
            _productService = productService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult All()
        {
            List<FavoriteListingVM> favorites = _favoriteService.GetFavorites()
                .Select(x => new FavoriteListingVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Product.Quantity,
                    Price = x.Product.Price,
                    Discount = x.Product.Discount,
                    BrandName = x.Product.Brand.BrandName,
                    CategoryName = x.Product.Category.CategoryName,
                }).ToList();
            return View(favorites);
        }

        public IActionResult My()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<FavoriteListingVM> favorites = _favoriteService.GetFavoritesByUser(userId)
                .Select(x => new FavoriteListingVM
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Product.Quantity,
                    Price = x.Product.Price,
                    Discount = x.Product.Discount,
                    BrandName = x.Product.Brand.BrandName,
                    CategoryName = x.Product.Category.CategoryName,
                }).ToList();

            return View("All", favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FavoriteCreateVM viewModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = this._productService.GetProductById(viewModel.ProductId);

            var created = _favoriteService.Create(viewModel.ProductId, userId);
            if (created)
            {
                return RedirectToAction("My");

            }
            return RedirectToAction();
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var favorite = _favoriteService.GetFavorite(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var detailsViewModel = new FavoriteDetailsVM()
            {
                Id = favorite.Id,
                UserId = favorite.UserId,
                User = favorite.User.UserName,
                ProductId = favorite.ProductId,
                Product = favorite.Product.ProductName,
                Picture = favorite.Product.Picture,
                Quantity = favorite.Product.Quantity,
                Price = favorite.Product.Price,
                Discount = favorite.Product.Discount,
                BrandName = favorite.Product.Brand.BrandName,
                CategoryName = favorite.Product.Category.CategoryName,
            };

            return View(detailsViewModel);
        }
        public ActionResult Delete(string id)
        {
            Favorite item = _favoriteService.GetFavorite(id);
            if (item == null)
            {
                return NotFound();
            }
            FavoriteDeleteVM favorite = new FavoriteDeleteVM()
            {
                Id = item.Id,
                Quantity = item.Product.Quantity,
                Price = item.Product.Price,
                Picture = item.Product.Picture,
                Discount = item.Product.Discount,
                BrandName = item.Product.Brand.BrandName,
                CategoryName = item.Product.Category.CategoryName,
                Product = item.Product.ProductName
            };
            return View(favorite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var isDeleted = _favoriteService.RemoveById(id);
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

    }
}
