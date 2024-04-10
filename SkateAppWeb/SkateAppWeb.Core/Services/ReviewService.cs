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
	public class ReviewService : IReviewService
	{
		private readonly ApplicationDbContext _context;

		public ReviewService(ApplicationDbContext context)
		{
			this._context = context;
		}
		public bool Create(string productId, string userId, string content, int rating, DateTime posted)
		{
			var product = _context.Products.SingleOrDefault(p => p.Id == productId);
			var user = _context.Users.SingleOrDefault(s => s.Id == userId);

			Review review = new Review
			{
				Posted = DateTime.Now,
				ProductId = productId,
				UserId = userId,
				Content = content,
				Rating = rating
			};

			_context.Products.Update(product);
			_context.Reviews.Add(review);

			return _context.SaveChanges() != 0;
		}

		public Review GetReview(string reviewId)
		{
			return _context.Reviews.Find(reviewId);
		}

		public List<Review> GetReviews()
		{
			return _context.Reviews.OrderByDescending(x => x.Posted).ToList();
		}

		public List<Review> GetReviewsbyUser(string userId)
		{
			return _context.Reviews.Where(x => x.UserId == userId).OrderByDescending(x => x.Posted).ToList();
		}

		public bool RemoveById(string reviewId)
		{
			var item = GetReview(reviewId);

			if (item == default(Review))
			{
				return false;
			}
			_context.Reviews.Remove(item);

			return _context.SaveChanges() != 0;
		}

		public bool Update(string id, string productId, string userId, string content, int rating, DateTime posted)
		{
			var product = _context.Products.SingleOrDefault(p => p.Id == productId);
			var item = _context.Reviews.Find(id);

			if (item == null)
			{
				return false;
			}

			item.Posted = DateTime.Now;
			item.Rating = rating;
			item.Content = content;

			_context.Reviews.Update(item);
			return _context.SaveChanges() != 0;
		}

        public IEnumerable<Review> GetReviewsByProductId(string productId)
        {
            return _context.Reviews.Where(x => x.ProductId == productId).OrderByDescending(x => x.Posted).ToList();
        }
    }
}
