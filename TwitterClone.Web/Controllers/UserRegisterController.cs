using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class UserRegisterController : Controller
    {
        private readonly IUserServiceAPI _userServiceAPI;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRegisterController(IUserServiceAPI userServiceAPI, UserManager<ApplicationUser> userManager)
        {
            _userServiceAPI = userServiceAPI;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
        
            var model = new UsersViewModel
            {
                Users = await _userServiceAPI.GetUsers()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserCreateViewModel
            {
                User = new UserRegisterModel()
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserCreateViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var doesUserExist = await _userManager.FindByEmailAsync(inputModel.User.EmailAddress);
                if (doesUserExist == null)
                {
                    var guidId = Guid.NewGuid();
                    var userId = guidId.ToString();
                    var registerUser = new UserDTO()
                    {
                        UserId = userId,
                        FirstName = inputModel.User.FirstName,
                        LastName = inputModel.User.LastName,
                        EmailAddress = inputModel.User.EmailAddress,
                        Password = inputModel.User.Password
                    };
                    await _userServiceAPI.RegisterUser(registerUser);

                    var identityUser = new ApplicationUser()
                    {
                        Id = registerUser.UserId,
                        UserName = registerUser.EmailAddress,
                        Email = registerUser.EmailAddress,
                    };

                    var createResult = await _userManager.CreateAsync(identityUser, registerUser.Password);

                    if (!createResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Could not create identity user");
                        return View("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "A user is already registered with that email.");
                    return View("Create", inputModel);
                }

                return RedirectToAction("Index", "UserLogin");
            }
            else
            {
                return View("Create", inputModel);
            }
        }

        [HttpGet("delete/{userEmail}")]
        public async Task<IActionResult> Delete(string userEmail)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user != null)
                {
                    var deleteUserDTO = new DeleteUserDTO()
                    {
                        Email = user.Email
                    };
                    var response = await _userServiceAPI.DeleteUser(deleteUserDTO);
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Could not delete user in userservice");
                        return RedirectToAction("Index");
                    }
                    await _userManager.DeleteAsync(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "was not able delete user");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
