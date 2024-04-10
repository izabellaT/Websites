using Microsoft.AspNetCore.Mvc;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Models.Brands;
using SkateWebApp.Infrastructure.Data.Domain;

namespace SkateAppWeb.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }
        public IActionResult Index()
        {
            List<BrandIndexVM> brands = _brandService.GetBrands()
                 .Select(brand => new BrandIndexVM
                 {
                     Id = brand.Id,
                     BrandName = brand.BrandName
                 }).ToList();
            return this.View(brands);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] BrandCreateVM bindingModel)
        {
            if (ModelState.IsValid)
            {
                _brandService.Create(bindingModel.BrandName);
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Edit(int id)
        {
            Brand brand = _brandService.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            BrandEditVM updatedBrand = new BrandEditVM()
            {
                Id = brand.Id,
                BrandName = brand.BrandName
            };
            return View(updatedBrand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BrandEditVM brand)
        {
            if (ModelState.IsValid)
            {
                var updated = _brandService.Update(id, brand.BrandName);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(brand);
        }

        public ActionResult Delete(int id)
        {
            Brand item = _brandService.GetBrandById(id);
            if (item == null)
            {
                return NotFound();
            }
            BrandDeleteVm brand = new BrandDeleteVm()
            {
                Id = item.Id,
                BrandName = item.BrandName
            };
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _brandService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
