using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwitterClone.Web.Data;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.Extensions;
using TwitterClone.Web.Models;
using TwitterClone.Web.RESTClients;
using TwitterClone.Web.ViewModels;

namespace TwitterClone.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IFileServiceAPI _fileServiceAPI;

        public ProfileController(AppIdentityDbContext context, IFileServiceAPI fileServiceAPI)
        {
            _context = context;
            _fileServiceAPI = fileServiceAPI;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string userId)
        {
            var userInDb = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (userInDb == null)
                return NotFound("You have to be logged in.");

            if (userInDb.UserName != User.Identity.Name)
            {
                ModelState.AddModelError("", "Unauthorized, you have to login first.");
                return RedirectToAction("Index", "Home");
            }

            var byteImageFile = await _fileServiceAPI.GetUserProfileImage(userInDb.Id);
            var model = new ProfileImageFileModel { UserId = userInDb.Id };

            if (byteImageFile != null)
            {
                model.HasLogo = byteImageFile.Value != null && byteImageFile.Value.Length > 0;
                model.MimeType = byteImageFile.Value.GetMimeTypeFromImageBytes();
                model.Base64Image = model.HasLogo ? Convert.ToBase64String(byteImageFile.Value) : null;
            }

            return View(model);
        }

        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadAsync(ProfileImageFileModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Base64Image) || model.Base64Image.Length < 7)
            {
                ModelState.AddModelError(string.Empty, "Please upload an image.");
                return View("Index", model);
            }

            var base64String = model.Base64Image.Split(',')[1];
            if (!base64String.IsBase64String())
            {
                ModelState.AddModelError(string.Empty, "The image could not be processed.");
                return View("Index", model);
            }

            var bytes = Convert.FromBase64String(base64String);
            if (!bytes.IsValidImage())
            {
                ModelState.AddModelError(string.Empty, "Unsupported image format.");
                return View("Index", model);
            }

            var bytePart = new ByteArrayPart(bytes, "image", "application/json");

            var response = await _fileServiceAPI.UploadProfileImage(model.UserId, bytePart);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError(string.Empty, "User already has a profile picture, delete it to upload new");
                return View("Index", model);
            }

            return RedirectToAction("Index", new { userId = model.UserId });
        }

        [HttpGet("delete-profile-image")]
        public async Task<IActionResult> DeleteProfileImage(string userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                ModelState.AddModelError("", "Could not find employee.");
                return RedirectToAction("index", "profile", new { id = userId });
            }

            var response = await _fileServiceAPI.DeleteProfileImage(userId);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Was not able to delete profile image.");
            }

            return RedirectToAction("index", "profile", new { userId = userId });
        }
    }
}
