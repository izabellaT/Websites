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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public dynamic GetProductById(string productId)
        {
            return _context.Products.Find(productId);
        }

        public List<T> GetProducts<T>() where T : class
        {
            return _context.Products.OfType<T>().ToList();
        }

        public List<Product> GetProducts(string searchStringBrandName, string searchStringCategoryName, string searchStringProductName,
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice
            ,string sortingAtoZName, string sortingZtoAName)
        {

            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringCategoryName) && !String.IsNullOrEmpty(searchStringBrandName) && !String.IsNullOrEmpty(searchStringProductName)
                && !String.IsNullOrEmpty(searchStringColor) && !String.IsNullOrEmpty(searchIntSize.ToString()) && !String.IsNullOrEmpty(searchIntPrice.ToString()))
            {
                products = products.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower()) && x.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower())
                && x.ProductName.ToLower().Contains(searchStringProductName.ToLower()) && x.Color.ToLower().Contains(searchStringColor.ToLower())
                 && x.Size.ToString().Contains(searchIntSize.ToString()) && x.Price.ToString().Contains(searchIntPrice.ToString())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                products = products.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBrandName))
            {
                products = products.Where(x => x.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringProductName))
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(searchStringProductName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringColor))
            {
                products = products.Where(x => x.Color.ToLower().Contains(searchStringColor.ToLower())).ToList();
            }
            else if (searchIntSize != 0)
            {
                products = products.Where(x =>
                (searchIntSize >= 52 && searchIntSize <= 54 && x.Size >= searchIntSize && x.Size <= 54) ||
                (searchIntSize >= 55 && searchIntSize <= 58 && x.Size >= searchIntSize && x.Size <= 58) ||
                (searchIntSize >= 59 && x.Size >= searchIntSize)).ToList();
            }
            else if (searchIntPrice !=0)
            {
                products = products.Where(x =>
        (searchIntPrice <= 25 && x.Price <= 25) ||
        (searchIntPrice >= 26 && searchIntPrice <= 50 && x.Price >= 26 && x.Price <= 50) ||
        (searchIntPrice >= 51 && searchIntPrice <= 75 && x.Price >= 51 && x.Price <= 75) ||
        (searchIntPrice >= 76 && searchIntPrice <= 100 && x.Price >= 76 && x.Price <= 100) ||
        (searchIntPrice >= 101 && searchIntPrice <= 150 && x.Price >= 101 && x.Price <= 150) ||
        (searchIntPrice >= 151 && searchIntPrice <= 200 && x.Price >= 151 && x.Price <= 200) ||
        (searchIntPrice >= 201 && searchIntPrice <= 300 && x.Price >= 201 && x.Price <= 300) ||
        (searchIntPrice >= 301 && x.Price >= 301)).ToList();
			}
            else if (sortingLowestPrice !=0)
            {
                products = products.Where(x => sortingLowestPrice >= 1 && x.Price >= 1).OrderByDescending(x => x.Price).ToList();
            }
            else if (sortingHighestPrice != 0)
            {
                products = products.Where(x => sortingHighestPrice >= 1 && x.Price >= 1).OrderBy(x => x.Price).ToList();
            }
            else if (!String.IsNullOrEmpty(sortingAtoZName))
            {
                products = products.Where(x => sortingAtoZName.Any()).OrderBy(x=>x.ProductName).Reverse().ToList();
            }
            else if (!String.IsNullOrEmpty(sortingZtoAName))
            {
                products = products.Where(x => sortingZtoAName.Any()).OrderBy(x => x.ProductName).ToList();
            }
            return products;
        }

        public bool RemoveProductById(string productId)
        {
            var item = GetProductById(productId);

            if (item == default(Product))
            {
                return false;
            }
            _context.Products.Remove(item);

            return _context.SaveChanges() != 0;
        }
    }
}
