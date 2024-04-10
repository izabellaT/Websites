using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateAppWeb.Core.Contracts
{
    public interface IBlogService
    {
        public bool Create(string userId, string content, string picture, string videoLink, DateTime posted);
        public List<Blog> GetBlogs();
        public Blog GetBlog(string blogId);
        public bool RemoveById(string blogId);
        public bool Update(string id, string content, string picture, string videoLink);
    }
}
