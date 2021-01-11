using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.Models;
using TwitterClone.Web.RESTClients;
using TwitterClone.Web.ViewModels;

namespace TwitterClone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServiceAPI _userServiceAPI;

        public UserController(IUserServiceAPI userServiceAPI)
        {
            _userServiceAPI = userServiceAPI;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Message"] = "Hello from webfrontend";

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
                User = new UserModel()
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserCreateViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var registerUser = new User()
                {
                    FirstName = inputModel.User.FirstName,
                    LastName = inputModel.User.LastName,
                    EmailAddress = inputModel.User.EmailAddress,
                    Password = inputModel.User.Password
                };
                await _userServiceAPI.RegisterCustomer(registerUser);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create", inputModel);
            }
        }
    }
}
