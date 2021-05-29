using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogNine.Models;
using BlogNine.BusinessManagers.Interfaces;

namespace BlogNine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogBusinessManager blogBusinessManager;
        public HomeController(IBlogBusinessManager blogBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
        }

        public IActionResult Index(string searchString, int? page)
        {
            return View(blogBusinessManager.GetIndexViewModel(searchString, page));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
