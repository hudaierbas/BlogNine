using BlogNine.Data.Models;
using BlogNine.Models.BlogViewModels;
using BlogNine.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNine.BusinessManagers.Interfaces
{
    public interface IBlogBusinessManager
    {
        Task<Blog> CreateBlog(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);
        IndexViewModel GetIndexViewModel(string searchString, int? page);
    }
}
