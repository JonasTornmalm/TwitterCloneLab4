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
    public class UserRegisterController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserRegisterController(UserDbContext context)
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
        [HttpGet]
        [Route("{userEmail}")]
        public async Task<IActionResult> GetByUserEmail(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == userEmail);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userInDb = await _context.Users.Where(x => x.EmailAddress == request.EmailAddress).FirstOrDefaultAsync();
        
                    if (userInDb != null)
                        return Ok(userInDb);
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
