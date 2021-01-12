﻿using Microsoft.AspNetCore.Identity;
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
                var registerUser = new UserDTO()
                {
                    FirstName = inputModel.User.FirstName,
                    LastName = inputModel.User.LastName,
                    EmailAddress = inputModel.User.EmailAddress,
                    Password = inputModel.User.Password
                };
                await _userServiceAPI.RegisterUser(registerUser);

                var doesUserExist = await _userManager.FindByEmailAsync(registerUser.EmailAddress);
                if (doesUserExist == null)
                {
                    var identityUser = new ApplicationUser()
                    {
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

                return RedirectToAction("Index", "UserRegister");
            }
            else
            {
                return View("Create", inputModel);
            }
        }
    }
}