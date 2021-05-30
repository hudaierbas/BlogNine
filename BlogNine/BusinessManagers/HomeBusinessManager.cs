﻿using BlogNine.BusinessManagers.Interfaces;
using BlogNine.Data.Models;
using BlogNine.Models.HomeViewModels;
using BlogNine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Linq;

namespace BlogNine.BusinessManagers
{
    public class HomeBusinessManager : IHomeBusinessManager
    {
        private readonly IPostService postService;
        private readonly IUserService userService;

        public HomeBusinessManager(IPostService postService, IUserService userService)
        {
            this.postService = postService;
            this.userService = userService;
        }

        public ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page)
        {
            if (authorId is null)
                return new BadRequestResult();

            var applicationUser = userService.Get(authorId);

            if (applicationUser is null)
                return new NotFoundResult();

            int pageSize = 6;
            int pageNumber = page ?? 1;

            var posts = postService.GetPosts(searchString ?? string.Empty)
                .Where(post => post.Published && post.Creator == applicationUser);

            return new AuthorViewModel
            {
                Author = applicationUser,
                Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }
    }
}
