using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.Data.Entities;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.Models;
using TwitterClone.Web.RESTClients;
using TwitterClone.Web.ViewModels;

namespace TwitterClone.Web.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly IUserServiceAPI _userServiceAPI;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserLoginController(IUserServiceAPI userServiceAPI, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userServiceAPI = userServiceAPI;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserLoginViewModel
            {
                User = new UserLoginModel()
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserLoginViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var loginUser = new UserDTO()
                {
                    EmailAddress = inputModel.User.EmailAddress,
                    Password = inputModel.User.Password
                };
                var result = await _userServiceAPI.TryLogin(loginUser);

                if (result == null)
                {
                    ModelState.AddModelError("", "Could not find a user.");
                    return View("Index");
                }

                var identityUser = await _userManager.FindByEmailAsync(result.EmailAddress);

                var signInResult = await _signInManager.PasswordSignInAsync(identityUser, result.Password, false, false);

                if (!signInResult.Succeeded)
                {
                    ModelState.AddModelError("", "Was not able to sign in.");
                    return View("Index");
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index", inputModel);
            }
        }
    }
}
