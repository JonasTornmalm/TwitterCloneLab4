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
    public class UserLoginController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserLoginController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel request)
        {
            if (ModelState.IsValid)
            {
                var userInDb = await _context.Users.Where(x => x.EmailAddress == request.EmailAddress && x.Password == request.Password).FirstOrDefaultAsync();
        
                if (userInDb != null)
                {
                    return Ok(userInDb);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
    }
}
