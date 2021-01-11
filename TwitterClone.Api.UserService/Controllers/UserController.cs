using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Api.UserService.Data;
using TwitterClone.Api.UserService.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterClone.Api.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserController(UserDbContext context)
        {
            _context = context;
        }

        //GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET api/<UserController>/5
        [HttpGet]
        [Route("{userId}", Name = "GetByUserId")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(c => c.UserId == userId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
