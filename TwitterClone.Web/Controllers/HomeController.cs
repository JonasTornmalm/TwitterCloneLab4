using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.Data;
using TwitterClone.Web.Models;
using TwitterClone.Web.ViewModels;

namespace TwitterClone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppIdentityDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if (user == null)
                return View(new IdentityViewModel());

            var model = new IdentityViewModel()
            {
                UserId = user.Id
            };

            return View(model);
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
