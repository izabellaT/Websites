using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Core.Services;
using SkateAppWeb.Models.Blogs;
using SkateAppWeb.Models.Brands;
using SkateAppWeb.Models.Categories;
using SkateAppWeb.Models.Reviews;
using SkateWebApp.Infrastructure.Data.Domain;
using System.Globalization;
using System.Security.Claims;

namespace SkateAppWeb.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            this._blogService = blogService;
        }

        public IActionResult All()
        {
            List<BlogListingVM> blogs = _blogService.GetBlogs()
                .Select(x => new BlogListingVM
                {
                    Id = x.Id,
                    Posted = x.Posted.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    Content = x.Content,
                    Picture = x.Picture,
                    VideoLink = x.VideoLink
                }).ToList();

            return View(blogs);
        }

        public IActionResult Create()
        {
            var blog = new BlogCreateVM();
           
            return View(blog);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogCreateVM viewModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var created = _blogService.Create(userId, viewModel.Content, viewModel.Picture, viewModel.VideoLink, viewModel.Posted);

            if (created)
            {
                return RedirectToAction("All");
            }
            return RedirectToAction();
        }
        public ActionResult Delete(string id)
        {
            Blog x = _blogService.GetBlog(id);
            if (x == null)
            {
                return NotFound();
            }
            BlogDeleteVM blog = new BlogDeleteVM()
            {
                Id = x.Id,
                Posted = x.Posted.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                UserId = x.UserId,
                User = x.User.UserName,
                Content = x.Content,
                Picture = x.Picture,
                VideoLink = x.VideoLink
            };
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var isDeleted = _blogService.RemoveById(id);
                if (isDeleted)
                {
                    return this.RedirectToAction("All");
                }
                return this.RedirectToAction("All");
            }
            catch
            {
                return this.RedirectToAction("All");
            }
        }
		[Authorize(Roles = "Administrator")]
		public IActionResult Edit(string id)
		{
			var blog = _blogService.GetBlog(id);

			var viewModel = new BlogCreateVM()
			{
				Id = blog.Id,
				UserId = blog.UserId,
				User = blog.User.UserName,
				Content = blog.Content,
				Picture = blog.Picture,
				VideoLink = blog.VideoLink
			};

			return View(viewModel);
		}
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(string id, BlogCreateVM editViewModel)
		{
			if (ModelState.IsValid)
			{
				var isUpdated = _blogService.Update(editViewModel.Id, editViewModel.Content, editViewModel.Picture, editViewModel.VideoLink);

				if (isUpdated)
				{
					return RedirectToAction("All", "Blogs");
				}
			}
			return View();
		}
	}
}
