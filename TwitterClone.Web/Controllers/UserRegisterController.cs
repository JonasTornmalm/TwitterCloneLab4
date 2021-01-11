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

        public UserRegisterController(IUserServiceAPI userServiceAPI)
        {
            _userServiceAPI = userServiceAPI;
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
                var registerUser = new UserDTO()
                {
                    FirstName = inputModel.User.FirstName,
                    LastName = inputModel.User.LastName,
                    EmailAddress = inputModel.User.EmailAddress,
                    Password = inputModel.User.Password
                };
                await _userServiceAPI.RegisterUser(registerUser);

                return RedirectToAction("Index", "UserRegister");
            }
            else
            {
                return View("Create", inputModel);
            }
        }
    }
}
