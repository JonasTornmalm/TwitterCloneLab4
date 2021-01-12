using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.RESTClients;
using TwitterClone.Web.ViewModels;

namespace TwitterClone.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessagingServiceAPI _messagingServiceAPI;
        public MessageController(IMessagingServiceAPI messagingServiceAPI)
        {
            _messagingServiceAPI = messagingServiceAPI;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new MessagesViewModel
            {
                Messages = await _messagingServiceAPI.GetMessages()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendAsync([FromForm] MessagesViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var message = new MessageDTO()
                {
                    Username = inputModel.SendMessage.Username,
                    Text = inputModel.SendMessage.Text
                };

                await _messagingServiceAPI.SendMessage(message);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "UserLogin");
            }
        }
    }
}
