using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;
using SkateAppWeb.Models.Products;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Drawing;
using System.Xml.Linq;

namespace SkateAppWeb.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly IAccessoryService accessoryService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public AccessoriesController(IAccessoryService _accessoryService, IProductService productService, IBrandService brandService, ICategoryService categoryService)
        {
            this.accessoryService = _accessoryService;
            this._productService = productService;
            this._brandService = brandService;
            this._categoryService = categoryService;
        }


        public IActionResult All()
        {
            List<ProductAllVM> viewModel = _productService.GetProducts<Accessory>()
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

            return View("~/Views/Products/All.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult All(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, 
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice,
            string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<Accessory>();
            List<ProductAllVM> viewModel = _productService.GetProducts(searchStringBrandName, searchStringCategoryName,
                searchStringProductName, searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice,
             sortingAtoZName, sortingZtoAName)
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

            return View("~/Views/Products/All.cshtml", viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateAccessory()
        {
            var product = new AccessoryCreateVM();
            product.Brands = _brandService.GetBrands().Select(x => new BrandPairVM()
            {
                Id = x.Id,
                Name = x.BrandName
            }).ToList();

            product.Categories = _categoryService.GetCategories().Select(x => new CategoryPairVM()
            {
                Id = x.Id,
                Name = x.CategoryName
            }).ToList();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateAccessory(AccessoryCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = accessoryService.CreateAccessory(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Price, createVm.Quantity, 
                    createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isCreated)
                {
                    return RedirectToAction("All", "Accessories");
                }
            }
            return View(createVm);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditAccessory(string id)
        {
            Accessory item = _productService.GetProductById(id);

            if (item == null)
            {
                return NotFound();
            }
            var product = new AccessoryCreateVM()
            {
                Id = item.Id,
                Name = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Price = item.Price,
                Discount = item.Discount,
                Picture = item.Picture,
                Color = item.Color,
                Quantity = item.Quantity,
                Material = item.Material,
                Description = item.Description,
                
            };

            product.Brands = _brandService.GetBrands().Select(x => new BrandPairVM()
            {
                Id = x.Id,
                Name = x.BrandName
            }).ToList();

            product.Categories = _categoryService.GetCategories().Select(x => new CategoryPairVM()
            {
                Id = x.Id,
                Name = x.CategoryName
            }).ToList();

            return View(product);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditAccessory(string id, AccessoryCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = accessoryService.UpdateAccessory(createVm.Id, createVm.Name, createVm.BrandId, createVm.CategoryId, 
                    createVm.Color, createVm.Price, createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isUpdated)
                {
                    return RedirectToAction("All");
                }
            }
            return View(createVm);
        }
    }
}
