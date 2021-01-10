using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            await _userServiceAPI.GetUsers();
            var model = new UsersViewModel();
            //var model = new UsersViewModel
            //{
            //    Users = await _userServiceAPI.GetUsers()
            //};
            //return View(model);
            return View(model);
        }
    }
}
