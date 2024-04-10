using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface IFavoriteService
    {
        public bool Create(string productId, string userId);
        public List<Favorite> GetFavorites();
        public List<Favorite> GetFavoritesByUser(string userId);
        public Favorite GetFavorite(string favoriteId);
        public bool RemoveById(string favoriteId);
    }
}
