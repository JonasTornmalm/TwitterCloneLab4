using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Api.MessagingService.Data;
using TwitterClone.Api.MessagingService.Data.Entities;
using TwitterClone.Api.MessagingService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterClone.Api.MessagingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly MessageDbContext _context;
        public MessagingController(MessageDbContext context)
        {
            _context = context;
        }
        // GET: api/<MessagingController>
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _context.Messages.ToListAsync();

            return Ok(messages);
        }

        // POST api/<MessagingController>
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel request)
        {
            if (ModelState.IsValid)
            {
                var message = new Message()
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Text = request.Text
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<MessagingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MessagingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
