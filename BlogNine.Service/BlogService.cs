﻿using BlogNine.Data;
using BlogNine.Data.Models;
using BlogNine.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
       
        public Blog GetBlog(int blogId)
        {
            return applicationDbContext.Blogs.FirstOrDefault(blog => blog.Id == blogId);
        }

        public IEnumerable<Blog> GetBlogs(ApplicationUser applicationUser)
        {
            return applicationDbContext.Blogs
                .Include(blog => blog.Creator)
                .Include(blog => blog.Approver)
                .Include(blog => blog.Posts)
                .Where(blog => blog.Creator == applicationUser);
        }

        public async Task<Blog> Add(Blog blog)
        {
            applicationDbContext.Add(blog);
            await applicationDbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> Update(Blog blog)
        {
            applicationDbContext.Update(blog);
            await applicationDbContext.SaveChangesAsync();
            return blog;
        }
    }
}
