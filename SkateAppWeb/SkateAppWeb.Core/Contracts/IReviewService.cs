using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
	public interface IReviewService
	{
		public bool Create(string productId, string userId, string content, int rating, DateTime posted);
		public List<Review> GetReviews();
		public List<Review> GetReviewsbyUser(string userId);
		public Review GetReview(string reviewId);
		public bool RemoveById(string reviewId);
		public bool Update(string id, string productId, string userId, string content, int rating, DateTime posted);
        IEnumerable<Review> GetReviewsByProductId(string productId);
    }
}
