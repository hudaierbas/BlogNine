using BlogNine.Authorization;
using BlogNine.BusinessManagers.Interfaces;
using BlogNine.Data.Models;
using BlogNine.Models.BlogViewModels;
using BlogNine.Models.HomeViewModels;
using BlogNine.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNine.BusinessManagers
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBlogService blogService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAuthorizationService authorizationService;

        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService, IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService)
        {
            this.userManager = userManager;
            this.blogService = blogService;
            this.webHostEnvironment = webHostEnvironment;
            this.authorizationService = authorizationService;
        }

        public IndexViewModel GetIndexViewModel(string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;
            var blogs = blogService.GetBlogs(searchString ?? string.Empty).Where(blogs => blogs.Published);

            return new IndexViewModel
            {
                Blogs = new StaticPagedList<Blog>(blogs.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, blogs.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }

        //CREATE NEW BLOG
        public async Task<Blog> CreateBlog(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blog blog = createViewModel.Blog;

            blog.Creator = await userManager.GetUserAsync(claimsPrincipal);
            blog.CreatedOn = DateTime.Now;
            blog.UpdatedOn = DateTime.Now;

            blog = await blogService.Add(blog);

            string webRootPath = webHostEnvironment.WebRootPath;
            string pathToImg = $@"{webRootPath}\UserFiles\Blogs\{blog.Id}\HeaderImage.jpg";

            EnsureFolder(pathToImg);

            using (var fileStream = new FileStream(pathToImg, FileMode.Create))
            {
                await createViewModel.BlogHeaderImage.CopyToAsync(fileStream);
            }

            return blog;
        }

        //UPDATE BLOG
        public async Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var blog = blogService.GetBlog(editViewModel.Blog.Id);

            if (blog is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, blog, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            blog.Published = editViewModel.Blog.Published;
            blog.Title = editViewModel.Blog.Title;
            blog.Content = editViewModel.Blog.Content;
            blog.UpdatedOn = DateTime.Now;

            if(editViewModel.BlogHeaderImage != null)
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                string pathToImg = $@"{webRootPath}\UserFiles\Blogs\{blog.Id}\HeaderImage.jpg";

                EnsureFolder(pathToImg);

                using (var fileStream = new FileStream(pathToImg, FileMode.Create))
                {
                    await editViewModel.BlogHeaderImage.CopyToAsync(fileStream);
                }
            }

            return new EditViewModel
            {
                Blog = await blogService.Update(blog)
            };
        }

        //GET BLOG DATA TO EDIT
        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var blogId = id.Value;
            var blog = blogService.GetBlog(blogId);

            if (blog is null)
                return new NotFoundResult();

            var authorizationResult = await authorizationService.AuthorizeAsync(claimsPrincipal, blog, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);
            
            return new EditViewModel
            {
                Blog = blog
            };

        }

        private ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }
    }
}
