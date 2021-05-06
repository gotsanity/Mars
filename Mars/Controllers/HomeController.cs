using Mars.Data;
using Mars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _ctx;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _ctx = applicationDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogPost> posts = _ctx.Posts.Include(m => m.User).ToList();

            return View(posts);
        }

        public IActionResult Detail(int id)
        {
            BlogPost post = _ctx.Posts.Include(m => m.User).Where(p => p.BlogPostID == id).FirstOrDefault();

            return View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
