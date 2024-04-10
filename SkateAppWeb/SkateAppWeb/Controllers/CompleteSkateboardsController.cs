using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;
using SkateAppWeb.Models.Products;
using SkateWebApp.Infrastructure.Data.Domain;
using static SkateAppWeb.Controllers.CompleteSkateboardsController;

namespace SkateAppWeb.Controllers
{
    public class CompleteSkateboardsController : Controller
    {
            private readonly ICompleteSkateboardService _completeSkateboardService;
            private readonly IProductService _productService;
            private readonly IBrandService _brandService;
            private readonly ICategoryService _categoryService;
            private readonly IComponentService _componentsService;

        public CompleteSkateboardsController(ICompleteSkateboardService completeSkateboardService, IProductService productService, IBrandService brandService, ICategoryService categoryService, IComponentService componentService)
            {
                this._completeSkateboardService = completeSkateboardService;
                this._productService = productService;
                this._brandService = brandService;
                this._categoryService = categoryService;
                this._componentsService = componentService;
        }

        public IActionResult All()
        {
            List<ProductAllVM> completeSkateboardViewModel = _productService.GetProducts<CompleteSkateboard>()
               .Select(completeSkateboard => new ProductAllVM
               {
                   Id = completeSkateboard.Id,
                   BrandId = completeSkateboard.BrandId,
                   BrandName = completeSkateboard.Brand.BrandName,
                   CategoryId = completeSkateboard.CategoryId,
                   CategoryName = completeSkateboard.Category.CategoryName,
                   Price = completeSkateboard.Price,
                   Discount = completeSkateboard.Discount,
                   Picture = completeSkateboard.Picture,
                   Description = completeSkateboard.PartialDescription,
                   Quantity = completeSkateboard.Quantity
               }).ToList();

            return View("~/Views/Products/All.cshtml", completeSkateboardViewModel);
        }

        [HttpPost]
        public IActionResult All(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, string searchStringColor, 
            int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice,
            string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<CompleteSkateboard>();
            List<ProductAllVM> completeSkateboardViewModel = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName,
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice,
             sortingAtoZName,  sortingZtoAName)
               .Select(completeSkateboard => new ProductAllVM
               {
                   Id = completeSkateboard.Id,
                   BrandId = completeSkateboard.BrandId,
                   BrandName = completeSkateboard.Brand.BrandName,
                   CategoryId = completeSkateboard.CategoryId,
                   CategoryName = completeSkateboard.Category.CategoryName,
                   Price = completeSkateboard.Price,
                   Discount = completeSkateboard.Discount,
                   Picture = completeSkateboard.Picture,
                   Description = completeSkateboard.PartialDescription,
                   Quantity = completeSkateboard.Quantity
               }).ToList();

            return View("~/Views/Products/All.cshtml", completeSkateboardViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateCompleteSkateboard()
        {
            ViewBag.Wheels = _productService.GetProducts<Wheel>();
            ViewBag.Trucks = _productService.GetProducts<Truck>();

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


            var product = new CompleteSkateboardCreateVM();
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
        public IActionResult CreateCompleteSkateboard(CompleteSkateboardCreateVM createVm)
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
                    var isCreated = _completeSkateboardService.CreateCompleteSkateboard(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.WheelId,
                    createVm.BearingId, createVm.GriptapeId, createVm.TruckId,
            createVm.Color, createVm.Shape, createVm.Size, createVm.Price,
            createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                    if (isCreated)
                    {
                        return RedirectToAction("All", "Products");
                    }
                }
            }
            return View(createVm);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditCompleteSkateboard(string id)
        {
            CompleteSkateboard completeSkateboard = _productService.GetProductById(id);

            if (completeSkateboard == null)
            {
                return NotFound();
            }
            var product = new CompleteSkateboardCreateVM()
            {
                Id = completeSkateboard.Id,
                Name = completeSkateboard.ProductName,
                BrandId = completeSkateboard.BrandId,
                CategoryId = completeSkateboard.CategoryId,
                Price = completeSkateboard.Price,
                Discount = completeSkateboard.Discount,
                Picture = completeSkateboard.Picture,
                Color = completeSkateboard.Color,
                Size = completeSkateboard.Size,
                Shape = completeSkateboard.Shape,
                Material= completeSkateboard.Material,
                Description = completeSkateboard.Description,
                Quantity = completeSkateboard.Quantity,
                Wheel = completeSkateboard.Wheel,
                Truck = completeSkateboard.Truck,
                Bearing = completeSkateboard.Bearing,
                Griptape = completeSkateboard.Griptape,
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
            return View(product);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditCompleteSkateboard(string id, CompleteSkateboardCreateVM createVm)
        {
            if (ModelState.IsValid)
            {

                var isUpdated = _completeSkateboardService.UpdateCompleteSkateboard(createVm.Id, createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.WheelId,
                    createVm.BearingId, createVm.GriptapeId, createVm.TruckId,
            createVm.Color, createVm.Shape, createVm.Size, createVm.Price,
            createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isUpdated)
                {
                    return RedirectToAction("All");
                }
            }
            return View(createVm);
        }
    }
}
