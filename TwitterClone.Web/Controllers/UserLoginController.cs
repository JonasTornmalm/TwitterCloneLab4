using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.Data.Entities;

namespace TwitterClone.Web.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserLoginController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string userEmail)
        {
            _userManager.FindByEmailAsync(userEmail);
            return View();
        }
    }
}
