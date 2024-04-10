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
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Create(string productId, string userId)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == productId);
            var user = _context.Users.SingleOrDefault(s => s.Id == userId);

            Favorite favorite = new Favorite
            {
                ProductId = productId,
                UserId = userId,
            };

            _context.Favorites.Add(favorite);

            return _context.SaveChanges() != 0;
        }

        public Favorite GetFavorite(string favoriteId)
        {
            return _context.Favorites.Find(favoriteId);
        }

        public List<Favorite> GetFavorites()
        {
            return _context.Favorites.ToList();
        }

        public List<Favorite> GetFavoritesByUser(string userId)
        {
            return _context.Favorites.Where(x => x.UserId == userId).ToList();
        }

        public bool RemoveById(string favoriteId)
        {
            var item = GetFavorite(favoriteId);

            if (item == default(Favorite))
            {
                return false;
            }
            _context.Favorites.Remove(item);

            return _context.SaveChanges() != 0;
        }

    }
}
