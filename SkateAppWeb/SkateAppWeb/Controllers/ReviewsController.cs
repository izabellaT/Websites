using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Models.Orders;
using SkateAppWeb.Models.Reviews;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Globalization;
using System.Security.Claims;

namespace SkateAppWeb.Controllers
{
	[Authorize]
	public class ReviewsController : Controller
	{
		private readonly IReviewService _reviewService;
		private readonly IProductService _productService;

		public ReviewsController(IReviewService reviewService, IProductService productService)
		{
			this._reviewService = reviewService;
			_productService = productService;
		}

		[Authorize(Roles = "Administrator")]
		public IActionResult All()
		{
			List<ReviewListingVM> reviews = _reviewService.GetReviews()
				.Select(x => new ReviewListingVM
                {
					Id = x.Id,
					Posted = x.Posted.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
					UserId = x.UserId,
					User = x.User.UserName,
					ProductId = x.ProductId,
					Product = x.Product.ProductName,
					Content = x.Content,
					Rating = x.Rating,
                    Picture = x.Product.Picture,
                }).ToList();

			return View(reviews);
		}

		public IActionResult My()
		{
			string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

			List<ReviewListingVM> reviews = _reviewService.GetReviewsbyUser(userId)
				.Select(x => new ReviewListingVM
                {
					Id = x.Id,
					Posted = x.Posted.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
					UserId = x.UserId,
					User = x.User.UserName,
					ProductId = x.ProductId,
					Product = x.Product.ProductName,
					Content = x.Content,
					Rating = x.Rating,
                    Picture = x.Product.Picture,
                }).ToList();

			return View("All", reviews);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ReviewCreateVM viewModel)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

             if (viewModel.Content.Contains("fuck") || viewModel.Content.Contains("goddamn") || viewModel.Content.Contains("shit") ||
				viewModel.Content.Contains("stupid") || viewModel.Content.Contains("useless") || viewModel.Content.Contains("bullshit") ||
                viewModel.Content.Contains("ass") || viewModel.Content.Contains("damn it") || viewModel.Content	.Contains("trash") )
            {
                return RedirectToAction("Denied", "Reviews");
            }
			
			var created = _reviewService.Create(viewModel.ProductId, userId, viewModel.Content,viewModel.Rating, viewModel.Posted);

            if (created)
			{
				return RedirectToAction("My");

			}
			return RedirectToAction();
		}

        public IActionResult Denied()
        {
            return View();
        }


        public ActionResult Delete(string id)
		{
			Review item = _reviewService.GetReview(id);
			if (item == null)
			{
				return NotFound();
			}
			ReviewDeleteVM review = new ReviewDeleteVM()
			{
				Id = item.Id,
				Posted = item.Posted.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
				Content = item.Content,
				Rating = item.Rating,
				Picture = item.Product.Picture,
                User = item.User.UserName,
                Product = item.Product.ProductName
            };
			return View(review);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(string id, IFormCollection collection)
		{
			try
			{
				var isDeleted = _reviewService.RemoveById(id);
				if (isDeleted)
				{
					return this.RedirectToAction("My");
				}
				return this.RedirectToAction("My");
			}
			catch
			{
				return this.RedirectToAction("My");
			}
		}

		public IActionResult Edit(string id)
		{
			var review = _reviewService.GetReview(id);

			var viewModel = new ReviewCreateVM()
			{
				Id = review.Id,
				UserId = review.UserId,
				User = review.User.UserName,
				ProductId = review.ProductId,
				Content = review.Content,
				Rating = review.Rating
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(string id, ReviewCreateVM editViewModel)
		{
			if (ModelState.IsValid)
			{
				var isUpdated = _reviewService.Update(editViewModel.Id, editViewModel.ProductId, editViewModel.UserId, editViewModel.Content, editViewModel.Rating, editViewModel.Posted);

				if (isUpdated)
				{
					return RedirectToAction("My", "Reviews");
				}
			}
			return View();
		}
	}
}
