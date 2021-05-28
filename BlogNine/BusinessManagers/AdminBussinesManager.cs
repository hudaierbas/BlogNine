using BlogNine.BusinessManagers.Interfaces;
using BlogNine.Data.Models;
using BlogNine.Models.AdminViewModels;
using BlogNine.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNine.BusinessManagers
{
    public class AdminBussinesManager : IAdminBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBlogService blogService;
        public AdminBussinesManager(UserManager<ApplicationUser> userManager, IBlogService blogService)
        {
            this.userManager = userManager;
            this.blogService = blogService;
        }
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel {
                Blogs = blogService.GetBlogs(applicationUser)
            };

        }
    }
}
