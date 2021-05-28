using BlogNine.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogNine.Service.Interfaces
{
    public interface IBlogService
    {
        Blog GetBlog(int blogId);
        IEnumerable<Blog> GetBlogs(ApplicationUser applicationUser);
        Task<Blog> Add(Blog blog);

        Task<Blog> Update(Blog blog);
    }
}