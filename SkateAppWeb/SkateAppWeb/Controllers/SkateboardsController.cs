using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Products;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Drawing;
using System.IO.Pipelines;
using System.Security.Policy;
using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;

namespace SkateAppWeb.Controllers
{
    public class SkateboardsController : Controller
    {
        private readonly ISkateboardService _skateboardService;
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IComponentService _componentsService;

        public SkateboardsController(ISkateboardService skateboardService, IProductService productService, IBrandService brandService, ICategoryService categoryService, IComponentService componentService)
        {
            this._skateboardService = skateboardService;
            this._productService = productService;
            this._brandService = brandService;
            this._categoryService = categoryService;
            this._componentsService = componentService;
        }

        public IActionResult All()
        {
            List<ProductAllVM> skateboardViewModel = _productService.GetProducts<Skateboard>()
                .Select(skateboard => new ProductAllVM
                {
                    Id = skateboard.Id,
                    BrandId = skateboard.BrandId,
                    BrandName = skateboard.Brand.BrandName,
                    CategoryId = skateboard.CategoryId,
                    CategoryName = skateboard.Category.CategoryName,
                    Price = skateboard.Price,
                    Discount = skateboard.Discount,
                    Picture = skateboard.Picture,
                    Description = skateboard.FullDescription,
                    Quantity = skateboard.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", skateboardViewModel);
        }

        [HttpPost]
        public IActionResult All(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, string searchStringColor, 
            int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice,
            string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<Skateboard>();
            List<ProductAllVM> skateboardiewModel = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName, 
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice,
             sortingAtoZName, sortingZtoAName)
                .Select(skateboard => new ProductAllVM
                {
                    Id = skateboard.Id,
                    BrandId = skateboard.BrandId,
                    BrandName = skateboard.Brand.BrandName,
                    CategoryId = skateboard.CategoryId,
                    CategoryName = skateboard.Category.CategoryName,
                    Price = skateboard.Price,
                    Discount = skateboard.Discount,
                    Picture = skateboard.Picture,
                    Description = skateboard.FullDescription,
                    Quantity = skateboard.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", skateboardiewModel);
        }

        [Authorize]
        public IActionResult CreateSkateboard()
        {
            
            ViewBag.WheelId = new SelectList(_productService.GetProducts<Wheel>().Select(x => new WheelCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size,
                Picture = x.Picture
            }), "Id", "Name");
            ViewBag.TruckId = new SelectList(_productService.GetProducts<Truck>().Select(x => new TruckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size,
                Picture = x.Picture
            }), "Id", "Name");
            ViewBag.BearingId = new SelectList(_productService.GetProducts<Bearing>().Select(x => new BearingCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Picture = x.Picture
            }), "Id", "Name");
            ViewBag.GriptapeId = new SelectList(_productService.GetProducts<Griptape>().Select(x => new GriptapeCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Picture = x.Picture
            }), "Id", "Name");
            ViewBag.DeckId = new SelectList(_productService.GetProducts<Deck>().Select(x => new DeckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size,
                Picture = x.Picture
            }), "Id", "Name");
            ViewBag.AccessoryId = new SelectList(_productService.GetProducts<Accessory>().Select(x => new AccessoryCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Picture = x.Picture
            }), "Id", "Name");

            ViewBag.Wheels = _productService.GetProducts<Wheel>().Select(x => new WheelCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size,
                Picture = x.Picture
            });
            ViewBag.Trucks = _productService.GetProducts<Truck>().Select(x => new TruckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size,
                Picture = x.Picture
            });
            ViewBag.Decks = _productService.GetProducts<Deck>().Select(x => new DeckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size,
                Picture = x.Picture
            });
            var product = new SkateboardCreateVM();
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
        [Authorize]
        public IActionResult CreateSkateboard(SkateboardCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var components = this._componentsService.GetComponents();
                foreach (var item in components)
                {
                    var quantityOfComponent = item.Quantity;

                    if (components == null || quantityOfComponent < createVm.Quantity)
                    {
                        return RedirectToAction("Denied", "Skateboards");
                    }

                    var isCreated = _skateboardService.CreateSkateboard(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.WheelId, createVm.DeckId,
                        createVm.BearingId, createVm.GriptapeId, createVm.TruckId, createVm.Quantity);

                    if (isCreated)
                    {
                        return RedirectToAction("All", "Products");
                    }
                }
            }
            return View(createVm);

           
        }

        public IActionResult Denied()
        {
            return View();
        }

       [Authorize(Roles = "Administrator")]
        public IActionResult EditSkateboard(string id)
        {
            Skateboard skateboard = _productService.GetProductById(id);

            if (skateboard == null)
            {
                return NotFound();
            }
            var product = new SkateboardCreateVM()
            {
                Id = skateboard.Id,
                Name = skateboard.ProductName,
                BrandId = skateboard.BrandId,
                CategoryId = skateboard.CategoryId,
                Price = skateboard.Price,
                Quantity = skateboard.Quantity,
                Wheel = skateboard.Wheel,
                Truck = skateboard.Truck,
                Bearing = skateboard.Bearing,
                Griptape = skateboard.Griptape,
                Deck = skateboard.Deck
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
            
            var products = _productService.GetAllProducts();

            ViewBag.WheelId = new SelectList(_productService.GetProducts<Wheel>().Select(x => new WheelCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size
            }), "Id", "Name");
            ViewBag.TruckId = new SelectList(_productService.GetProducts<Truck>().Select(x => new TruckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size
            }), "Id", "Name");
            ViewBag.BearingId = new SelectList(_productService.GetProducts<Bearing>().Select(x => new BearingCreateVM
            {
                Name = x.ProductName,
                Id = x.Id
            }), "Id", "Name");
            ViewBag.GriptapeId = new SelectList(_productService.GetProducts<Griptape>().Select(x => new GriptapeCreateVM
            {
                Name = x.ProductName,
                Id = x.Id
            }), "Id", "Name");
            ViewBag.DeckId = new SelectList(_productService.GetProducts<Deck>().Select(x => new DeckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size
            }), "Id", "Name");
            ViewBag.AccessoryId = new SelectList(_productService.GetProducts<Accessory>().Select(x => new AccessoryCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
            }), "Id", "Name");

            ViewBag.Wheels = _productService.GetProducts<Wheel>().Select(x => new WheelCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size
            });
            ViewBag.Trucks = _productService.GetProducts<Truck>().Select(x => new TruckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size
            });
            ViewBag.Decks = _productService.GetProducts<Deck>().Select(x => new DeckCreateVM
            {
                Name = x.ProductName,
                Id = x.Id,
                Size = x.Size
            });

            return View("EditSkateboard", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditSkateboard(string id, SkateboardCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _skateboardService.UpdateSkateboard(createVm.Id, createVm.Name,  createVm.BrandId, createVm.CategoryId, createVm.WheelId, createVm.DeckId,
                    createVm.BearingId, createVm.GriptapeId, createVm.TruckId,createVm.Quantity);

                if (isUpdated)
                {
                    return RedirectToAction("All");
                }
            }
            return View(createVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Restore(SkateboardCreateVM viewModel)
        {
            Skateboard skateboard = _productService.GetProductById(viewModel.Id);
            skateboard.Price = skateboard.Wheel.Price + skateboard.Bearing.Price +
            skateboard.Truck.Price + skateboard.Griptape.Price +
            skateboard.Deck.Price + skateboard.Accessory.Price;

            skateboard.Picture = skateboard.Deck.Picture;

            return View("EditSkateboard", skateboard);
        }
    }
}
