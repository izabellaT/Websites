using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;
using SkateAppWeb.Models.Products;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipelines;
using System.Xml.Linq;

namespace SkateAppWeb.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ComponentsController : Controller
    {
        private readonly IComponentService _componentsService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        public ComponentsController(IComponentService componentsService, IProductService productService, IBrandService brandService, ICategoryService categoryService)
        {
            this._componentsService = componentsService;
            this._productService = productService;
            this._brandService = brandService;
            this._categoryService = categoryService;

        }

        [AllowAnonymous]
        public IActionResult All()
        {
            List<ProductAllVM> componentVM = _componentsService.GetComponents()
               .Select(product => new ProductAllVM
               {
                   Id = product.Id,
                   Name = product.ProductName,
                   BrandId = product.BrandId,
                   BrandName = product.Brand.BrandName,
                   CategoryId = product.CategoryId,
                   CategoryName = product .Category.CategoryName,
                   Price = product.Price,
                   Discount = product.Discount,
                   Picture = product.Picture,
                   Description = product.PartialDescription,
                   Quantity = product.Quantity
               }).ToList();

            return View("~/Views/Products/All.cshtml", componentVM);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult All(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, string searchStringColor,
            int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice, string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _componentsService.GetComponents();

            List<ProductAllVM> componentVM = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName,
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

            return View("~/Views/Products/All.cshtml", componentVM);
        }

        //------Trucks------

        [AllowAnonymous]
        public IActionResult AllTrucks()
        {
            List<ProductAllVM> truckVM = _productService.GetProducts<Truck>()
                .Select(truck => new ProductAllVM
                {
                    Id = truck.Id,
                    Name = truck.ProductName,
                    BrandId = truck.BrandId,
                    BrandName = truck.Brand.BrandName,
                    CategoryId = truck.CategoryId,
                    CategoryName = truck.Category.CategoryName,
                    Price = truck.Price,
                    Discount = truck.Discount,
                    Picture = truck.Picture,
                    Description = truck.PartialDescription,
                    Quantity = truck.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", truckVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllTrucks(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName,
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice, string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<Truck>();
            List<ProductAllVM> truckVM = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName, 
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice, sortingAtoZName, sortingZtoAName)
                .Select(truck=> new ProductAllVM
                {
                    Id = truck.Id,
                    Name = truck.ProductName,
                    BrandId = truck.BrandId,
                    BrandName = truck.Brand.BrandName,
                    CategoryId = truck.CategoryId,
                    CategoryName = truck.Category.CategoryName,
                    Price = truck.Price,
                    Discount = truck.Discount,
                    Picture = truck.Picture,
                    Description = truck.PartialDescription,
                    Quantity = truck.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", truckVM);
        }

        public IActionResult CreateTruck()
        {
            var product = new TruckCreateVM();
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
        public IActionResult CreateTruck(TruckCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _componentsService.CreateTruck(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color,createVm.Size, createVm.Count, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isCreated)
                {
                    return RedirectToAction("AllTrucks");
                }
            }
            return View();
        }

        public IActionResult EditTruck(string id)
        {
            Truck item = _productService.GetProductById(id);

            if (item == null)
            {
                return NotFound();
            }
            var product = new TruckCreateVM
            {
                Id = item.Id,
                Name = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Color = item.Color,
                Count = item.Count,
                Size = item.Size,   
                Price = item.Price,
                Quantity = item.Quantity,
                Material = item.Material,
                Description = item.Description,
                Discount = item.Discount,
                Picture = item.Picture
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
        public IActionResult EditTruck(string id, TruckCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _componentsService.UpdateTruck(createVm.Id, createVm.Name,createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Size, createVm.Count, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }

        //------Wheels------

        [AllowAnonymous]
        public IActionResult AllWheels()
        {
            List<ProductAllVM> wheelVM = _productService.GetProducts<Wheel>()
                .Select(wheel => new ProductAllVM
                {
                    Id = wheel.Id,
                    Name = wheel.ProductName,
                    BrandId = wheel.BrandId,
                    BrandName = wheel.Brand.BrandName,
                    CategoryId = wheel.CategoryId,
                    CategoryName = wheel.Category.CategoryName,
                    Price = wheel.Price,
                    Discount = wheel.Discount,
                    Picture = wheel.Picture,
                    Description = wheel.PartialDescription,
                    Quantity = wheel.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", wheelVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllWheels(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, 
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice, string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<Wheel>();
            List<ProductAllVM> wheelVM = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName,
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice, sortingAtoZName, sortingZtoAName)
                .Select(wheel => new ProductAllVM
                {
                    Id = wheel.Id,
                    Name = wheel.ProductName,
                    BrandId = wheel.BrandId,
                    BrandName = wheel.Brand.BrandName,
                    CategoryId = wheel.CategoryId,
                    CategoryName = wheel.Category.CategoryName,
                    Price = wheel.Price,
                    Discount = wheel.Discount,
                    Picture = wheel.Picture,
                    Description = wheel.PartialDescription,
                    Quantity = wheel.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", wheelVM);
        }

        public IActionResult CreateWheel()
        {
            var product = new WheelCreateVM();
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
        public IActionResult CreateWheel(WheelCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _componentsService.CreateWheel(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Count, createVm.Size, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isCreated)
                {
                    return RedirectToAction("AllWheels");
                }
            }
            return RedirectToAction("CreateWheel");
        }

        public IActionResult EditWheel(string id)
        {
            Wheel item = _productService.GetProductById(id);

            if (item == null)
            {
                return NotFound();
            }
            var product = new WheelCreateVM
            {
                Id = item.Id,
                Name = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Color = item.Color,
                Count = item.Count,
                Size = item.Size,
                Price = item.Price,
                Quantity = item.Quantity,
                Material = item.Material,
                Description = item.Description,
                Discount = item.Discount,
                Picture = item.Picture
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
        public IActionResult EditWheel(string id, WheelCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _componentsService.UpdateWheel(createVm.Id, createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Count, createVm.Size, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }
        //------Bearing------

        [AllowAnonymous]
        public IActionResult AllBearings()
        {
            List<ProductAllVM> bearingVM = _productService.GetProducts<Bearing>()
                .Select(bearing => new ProductAllVM
                {
                    Id = bearing.Id,
                    Name = bearing.ProductName,
                    BrandId = bearing.BrandId,
                    BrandName = bearing.Brand.BrandName,
                    CategoryId = bearing.CategoryId,
                    CategoryName = bearing.Category.CategoryName,
                    Price = bearing.Price,
                    Discount = bearing.Discount,
                    Picture = bearing.Picture,
                    Description = bearing.PartialDescription,
                    Quantity = bearing.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", bearingVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllBearings(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, 
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice, string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<Bearing>();

            List<ProductAllVM> bearingVM = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName, 
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice, sortingAtoZName, sortingZtoAName)
                .Select(bearing => new ProductAllVM
                {
                    Id = bearing.Id,
                    Name = bearing.ProductName,
                    BrandId = bearing.BrandId,
                    BrandName = bearing.Brand.BrandName,
                    CategoryId = bearing.CategoryId,
                    CategoryName = bearing.Category.CategoryName,
                    Price = bearing.Price,
                    Discount = bearing.Discount,
                    Picture = bearing.Picture,
                    Description = bearing.PartialDescription,
                    Quantity = bearing.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", bearingVM);
        }

        public IActionResult CreateBearing()
        {
            var product = new BearingCreateVM();
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
        public IActionResult CreateBearing(BearingCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _componentsService.CreateBearing(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Count, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isCreated)
                {
                    return RedirectToAction("AllBearings");
                }
            }
            return View();
        }

        public IActionResult EditBearing(string id)
        {
            Bearing item = _productService.GetProductById(id);

            if (item == null)
            {
                return NotFound();
            }
            var product = new BearingCreateVM
            {
                Id = item.Id,
                Name = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Color = item.Color,
                Count = item.Count,
                Price = item.Price,
                Quantity = item.Quantity,
                Material = item.Material,
                Description = item.Description,
                Discount = item.Discount,
                Picture = item.Picture
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
        public IActionResult EditBearing(string id, BearingCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _componentsService.UpdateBearing(createVm.Id, createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Count, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }
        //-----Decks------

        [AllowAnonymous]
        public IActionResult AllDecks()
        {
            List<ProductAllVM> deckVM = _productService.GetProducts<Deck>()
                .Select(deck => new ProductAllVM
                {
                    Id = deck.Id,
                    Name = deck.ProductName,
                    BrandId = deck.BrandId,
                    BrandName = deck.Brand.BrandName,
                    CategoryId = deck.CategoryId,
                    CategoryName = deck.Category.CategoryName,
                    Price = deck.Price,
                    Discount = deck.Discount,
                    Picture = deck.Picture,
                    Description = deck.PartialDescription,
                    Quantity = deck.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", deckVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllDecks(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, 
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice, string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<Deck>();
            List<ProductAllVM> deckVM = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName, 
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice, sortingAtoZName, sortingZtoAName)
                .Select(deck => new ProductAllVM
                {
                    Id = deck.Id,
                    Name = deck.ProductName,
                    BrandId = deck.BrandId,
                    BrandName = deck.Brand.BrandName,
                    CategoryId = deck.CategoryId,
                    CategoryName = deck.Category.CategoryName,
                    Price = deck.Price,
                    Discount = deck.Discount,
                    Picture = deck.Picture,
                    Description = deck.PartialDescription,
                    Quantity = deck.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", deckVM);
        }

        public IActionResult CreateDeck()
        {
            var product = new DeckCreateVM();
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
        public IActionResult CreateDeck(DeckCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _componentsService.CreateDeck(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Shape, createVm.Size, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isCreated)
                {
                    return RedirectToAction("AllDecks");
                }
            }
            return View();
        }

        public IActionResult EditDeck(string id)
        {
            Deck item = _productService.GetProductById(id);

            if (item == null)
            {
                return NotFound();
            }
            var product = new DeckCreateVM
            {
                Id = item.Id,
                Name = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Color = item.Color,
                Shape = item.Shape,
                Size = item.Size,
                Price = item.Price,
                Quantity = item.Quantity,
                Material = item.Material,
                Description = item.Description,
                Discount = item.Discount,
                Picture = item.Picture
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
        public IActionResult EditDeck(string id, DeckCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _componentsService.UpdateDeck(createVm.Id, createVm.Name, createVm.BrandId, 
                    createVm.CategoryId, createVm.Color, createVm.Shape, createVm.Size, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }

        //------Griptapes-----

        [AllowAnonymous]
        public IActionResult AllGriptapes()
        {
            List<ProductAllVM> griptapeVM = _productService.GetProducts<Griptape>()
                .Select(griptape => new ProductAllVM
                {
                    Id = griptape.Id,
                    Name = griptape.ProductName,
                    BrandId = griptape.BrandId,
                    BrandName = griptape.Brand.BrandName,
                    CategoryId = griptape.CategoryId,
                    CategoryName = griptape.Category.CategoryName,
                    Price = griptape.Price,
                    Discount = griptape.Discount,
                    Picture = griptape.Picture,
                    Description = griptape.PartialDescription,
                    Quantity = griptape.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", griptapeVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllGriptapes(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName, 
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice, string sortingAtoZName, string sortingZtoAName)
        {
            var oldProducts = _productService.GetProducts<Griptape>();
            List<ProductAllVM> griptapeVM = _productService.GetProducts(searchStringBrandName, searchStringCategoryName, searchStringProductName,
                searchStringColor, searchIntSize, searchIntPrice, sortingLowestPrice, sortingHighestPrice, sortingAtoZName, sortingZtoAName)
                .Select(griptape => new ProductAllVM
                {
                    Id = griptape.Id,
                    Name = griptape.ProductName,
                    BrandId = griptape.BrandId,
                    BrandName = griptape.Brand.BrandName,
                    CategoryId = griptape.CategoryId,
                    CategoryName = griptape.Category.CategoryName,
                    Price = griptape.Price,
                    Discount = griptape.Discount,
                    Picture = griptape.Picture,
                    Description = griptape.PartialDescription,
                    Quantity = griptape.Quantity
                }).ToList();

            return View("~/Views/Products/All.cshtml", griptapeVM);
        }

        public IActionResult CreateGriptape()
        {
            var product = new GriptapeCreateVM();
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
        public IActionResult CreateGriptape(GriptapeCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _componentsService.CreateGriptape(createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isCreated)
                {
                    return RedirectToAction("AllGriptapes");
                }
            }
            return View();
        }

        public IActionResult EditGriptape(string id)
        {
            Griptape item = _productService.GetProductById(id);

            if (item == null)
            {
                return NotFound();
            }
            var product = new GriptapeCreateVM
            {
                Id = item.Id,
                Name = item.ProductName,
                BrandId = item.BrandId,
                CategoryId = item.CategoryId,
                Color = item.Color,
                Price = item.Price,
                Quantity = item.Quantity,
                Material = item.Material,
                Description = item.Description,
                Discount = item.Discount,
                Picture = item.Picture
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
        public IActionResult EditGriptape(string id, GriptapeCreateVM createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _componentsService.UpdateGriptape(createVm.Id, createVm.Name, createVm.BrandId, createVm.CategoryId, createVm.Color, createVm.Price,
                createVm.Quantity, createVm.Material, createVm.Description, createVm.Discount, createVm.Picture);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }
    }
}
