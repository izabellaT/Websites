using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        public List<T> GetProducts<T>() where T : class;
        public dynamic GetProductById(string productId);
        public bool RemoveProductById(string productId);
        public List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName, string searchStringProductName, 
            string searchStringColor, int searchIntSize, int searchIntPrice, int sortingLowestPrice, int sortingHighestPrice,
            string sortingAtoZName, string sortingZtoAName);
    }
}
