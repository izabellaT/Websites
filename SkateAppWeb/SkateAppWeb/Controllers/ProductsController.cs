using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Orders;
using SkateAppWeb.Models.Products;
using SkateAppWeb.Models.Reviews;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Globalization;

namespace SkateAppWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;
        public ProductsController(IProductService productService, IReviewService reviewService)
        {
            this._productService = productService;
            _reviewService = reviewService;
        }

        public IActionResult All()
        {
            List<ProductAllVM> productVm = _productService.GetAllProducts()
               .Select(product => new ProductAllVM
               {
                   Id = product.Id,
                   Name = product.ProductName,
                   BrandId = product.BrandId,
                   BrandName = product.Brand.BrandName,
                   CategoryId = product.CategoryId,
                   CategoryName = product.Category.CategoryName,
                   Price = product.Price,
                   Discount = product.Discount,
                   Picture = product.Picture,
                   Description = product.PartialDescription,
                   Quantity = product.Quantity
               }).ToList();

            return View(productVm);
        }

        public IActionResult Search(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName,
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice, string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetAllProducts();

            List<ProductAllVM> productVm = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName, 
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice, sortingAtoZName, sortingZtoAName)
               .Select(product => new ProductAllVM
               {
                   Id = product.Id,
                   Name = product.ProductName,
                   BrandId = product.BrandId,
                   BrandName = product.Brand.BrandName,
                   CategoryId = product.CategoryId,
                   CategoryName = product.Category.CategoryName,
                   Price = product.Price,
                   Discount = product.Discount,
                   Picture = product.Picture,
                   Description = product.PartialDescription,
                   Quantity = product.Quantity
               }).ToList();

            return View(nameof(All),productVm);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AllTable()
        {
            List<ProductAllTableVM> productVm = _productService.GetAllProducts()
               .Select(product => new ProductAllTableVM
               {
                   Id = product.Id,
                   Name = product.ProductName,
                   BrandId = product.BrandId,
                   BrandName = product.Brand.BrandName,
                   CategoryId = product.CategoryId,
                   CategoryName = product.Category.CategoryName,
                   Price = product.Price,
                   Discount = product.Discount,
                   Picture = product.Picture,
                   Quantity = product.Quantity
               }).ToList();

			return View(productVm);
        }

		public ActionResult Delete(string id)
		{
			Product item = _productService.GetProductById(id);
			if (item == null)
			{
				return NotFound();
			}
			ProductDeleteVM product = new ProductDeleteVM()
			{
				Id = item.Id,
				Name = item.ProductName,
				BrandId = item.BrandId,
				BrandName = item.Brand.BrandName,
				CategoryId = item.CategoryId,
				CategoryName = item.Category.CategoryName,
				Price = item.Price,
				Discount = item.Discount,
				Picture = item.Picture,
				Quantity = item.Quantity
			};
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Administrator")]
		public ActionResult Delete(string id, IFormCollection collection)
		{
			try
			{
				var isDeleted = _productService.RemoveProductById(id);
				if (isDeleted)
				{
					return this.RedirectToAction("All", "Products");
				}
				return this.RedirectToAction("All", "Products");
			}
			catch
			{
				return this.RedirectToAction("All", "Products");
			}
		}

		public IActionResult Details(string id)
            {
                Product product = _productService.GetProductById(id);

                if (product == null)
                {
                    return NoContent();
                }

            IEnumerable<Review> reviews = _reviewService.GetReviewsByProductId(id);

            ProductDetailsVM detailsViewModel = new ProductDetailsVM
                {
                    Id = product.Id,
                    Name = product.ProductName,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.BrandName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.CategoryName,
                    Price = product.Price,
                    Discount = product.Discount,
                    Picture = product.Picture,
                    Description = product.FullDescription,
                    Quantity = product.Quantity,
                    Reviews = reviews,
            };
                return View("~/Views/Products/Details.cshtml", detailsViewModel);
            }
    public IActionResult Information()
    {
        return View();
    }
        public IActionResult Contacts()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Guide()
        {
            return View();
        }
        public IActionResult MostAsked()
        {
            return View();
        }
    }
 }
