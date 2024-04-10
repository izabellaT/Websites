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
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;
        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Brand GetBrandById(int brandId)
        {
            return _context.Brands.Find(brandId);
        }

        public List<Brand> GetBrands()
        {
            List<Brand> brands = _context.Brands.ToList();
            return brands;
        }

        public List<Product> GetProductByBrand(int brandId)
        {
            return _context.Products.Where(x => x.BrandId == brandId).ToList();
        }

        public bool Create(string brandName)
        {
            Brand item = new Brand()
            {
                BrandName = brandName,

            };
            _context.Brands.Add(item);
            return _context.SaveChanges() != 0;
        }
        public bool RemoveById(int brandId)
        {
            var brand = GetBrandById(brandId);
            if (brand == default(Brand))
            {
                return false;
            }
            _context.Remove(brand);
            return _context.SaveChanges() != 0;
        }
        public bool Update(int brandId, string name)
        {
            var brand = GetBrandById(brandId);
            if (brand == default(Brand))
            {
                return false;
            }
            brand.BrandName = name;
            _context.Update(brand);
            return _context.SaveChanges() != 0;
        }
    }
}
