using SkateAppWeb.Core.Contracts;
using SkateAppWeb.Infrastructure.Data;
using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Create(string userId, string content, string picture, string videoLink, DateTime posted)
        {
            var user = _context.Users.SingleOrDefault(s => s.Id == userId);

            Blog blog = new Blog
            {
                Posted = DateTime.Now,
                UserId = userId,
                Content = content,
                Picture = picture,
                VideoLink = videoLink
            };
            _context.Blogs.Add(blog);

            return _context.SaveChanges() != 0;
        }

        public Blog GetBlog(string blogId)
        {
            return _context.Blogs.Find(blogId);
        }

        public List<Blog> GetBlogs()
        {
            return _context.Blogs.OrderByDescending(x => x.Posted).ToList();
        }

        public bool RemoveById(string blogId)
        {
            var item = GetBlog(blogId);

            if (item == default(Blog))
            {
                return false;
            }
            _context.Blogs.Remove(item);

            return _context.SaveChanges() != 0;
        }

        public bool Update(string id,string content, string picture, string videoLink)
        {
            var item = _context.Blogs.Find(id);

            if (item == null)
            {
                return false;
            }
            item.Content = content;
            item.Picture = picture;
            item.VideoLink = videoLink;

            _context.Blogs.Update(item);
            return _context.SaveChanges() != 0;
        }
    }
}
