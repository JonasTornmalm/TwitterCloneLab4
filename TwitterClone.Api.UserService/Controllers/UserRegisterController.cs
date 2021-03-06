﻿using Microsoft.AspNetCore.Http;
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

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // insert user
                    var createUser = new User()
                    {
                        Id = request.UserId,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        EmailAddress = request.EmailAddress,
                        Password = request.Password
                    };

                    _context.Users.Add(createUser);
                    await _context.SaveChangesAsync();
        
                    return Ok();
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
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserDTO deleteUserDto)
        {
            try
            {
                var userToRemove = await _context.Users.SingleOrDefaultAsync(x => x.EmailAddress == deleteUserDto.Email);
                if (userToRemove is null)
                {
                    return NotFound();
                }
                _context.Users.Remove(userToRemove);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
