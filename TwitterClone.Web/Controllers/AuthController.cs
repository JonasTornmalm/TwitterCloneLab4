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
    public class AuthController : Controller
    {
        private readonly IUserServiceAPI _userServiceAPI;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IUserServiceAPI userServiceAPI, UserManager<ApplicationUser> userManager)
        {
            _userServiceAPI = userServiceAPI;
            _userManager = userManager;
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
        public async Task<IActionResult> Authenticate([FromForm] UserLoginViewModel inputModel)
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
                if (_userManager.FindByEmailAsync(result.EmailAddress) == null)
                {
                    var identityUser = new ApplicationUser()
                    {
                        UserName = result.EmailAddress,
                        Email = result.EmailAddress,
                    };

                    var createResult = await _userManager.CreateAsync(identityUser, result.Password);

                    if (!createResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Could not create identity user");
                        return View("Index");
                    }
                }


                return RedirectToAction("Login", "UserLogin", new { userEmail = result.EmailAddress });
            }
            else
            {
                return View("Index", inputModel);
            }
        }
    }
}
