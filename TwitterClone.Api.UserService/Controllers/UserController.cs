using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Api.UserService.Data;
using TwitterClone.Api.UserService.Data.Entities;
using TwitterClone.Api.UserService.Models;

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
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        // GET api/<UserController>/5
        //[HttpGet]
        //[Route("{userId}", Name = "GetByUserId")]
        //public async Task<IActionResult> GetByUserId(string userId)
        //{
        //    var customer = await _context.Users.FirstOrDefaultAsync(c => c.UserId == userId);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(customer);
        //}

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // insert customer
                    var createUser = new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        EmailAddress = request.EmailAddress,
                        Password = request.Password
                    };

                    _context.Users.Add(createUser);
                    await _context.SaveChangesAsync();
        
                    return Ok();
                    // return result
                    //return CreatedAtRoute("GetByCustomerId", new { customerId = customer.CustomerId }, customer);
                }
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
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
