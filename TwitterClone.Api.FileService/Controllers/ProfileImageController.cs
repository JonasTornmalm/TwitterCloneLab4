using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Api.FileService.Data;
using TwitterClone.Api.FileService.Data.Entities;
using TwitterClone.Api.FileService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterClone.Api.FileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly FileDbContext _context;
        public ProfileImageController(FileDbContext context)
        {
            _context = context;
        }
        // GET: api/<FileController>
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserProfileImage(string userId)
        {
            var userProfileImage = await _context.ProfileImageFiles.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (userProfileImage == null)
                return NotFound("Could not find profile image for user");

            var byteArrayPart = new ByteArrayPart(userProfileImage.ProfileImageBytes, userProfileImage.FileName, userProfileImage.ContentType);

            return Ok(byteArrayPart);
        }

        // POST api/<FileController>
        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> UploadProfileImage(string userId, ByteArrayPart request)
        {
            var profileImageInDb = await _context.ProfileImageFiles.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (profileImageInDb != null)
                return BadRequest("User already has a profile picture");

            var guidId = Guid.NewGuid();
            var uploadImage = new ProfileImageFile()
            {
                Id = guidId.ToString(),
                UserId = userId,
                ProfileImageBytes = request.Value,
                FileName = request.FileName,
                ContentType = request.ContentType
            };

            _context.ProfileImageFiles.Add(uploadImage);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<FileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FileController>/5
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteProfileImage(string userId)
        {
            var profileImageInDb = await _context.ProfileImageFiles.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (profileImageInDb == null)
                return BadRequest("No profile image found");

            _context.ProfileImageFiles.Remove(profileImageInDb);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
