using BlogNine.Data;
using BlogNine.Data.Models;
using BlogNine.Service.Interfaces;
using System.Threading.Tasks;

namespace BlogNine.Service
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BlogService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<Blog> Add(Blog blog)
        {
            applicationDbContext.Add(blog);
            await applicationDbContext.SaveChangesAsync();
            return blog;
        }
    }
}
