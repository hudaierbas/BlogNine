﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogNine.BusinessManagers;
using BlogNine.BusinessManagers.Interfaces;
using BlogNine.Models.BlogViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogNine.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogBusinessManager blogBusinessManager;

        public BlogController(IBlogBusinessManager blogBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await blogBusinessManager.GetEditViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createViewModel)
        {
            await blogBusinessManager.CreateBlog(createViewModel, User);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult = await blogBusinessManager.UpdateBlog(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Index", "Admin");
            //return RedirectToAction("Edit", new { editViewModel.Blog.Id });

            return actionResult.Result;

        }
            
    }
}