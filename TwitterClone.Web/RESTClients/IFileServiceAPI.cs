using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.Models;

namespace TwitterClone.Web.RESTClients
{
    public interface IFileServiceAPI
    {
        [Get("/profileimage/{userId}")]
        Task<ByteArrayPart> GetUserProfileImage(string userId);

        [Post("/profileimage/{userId}")]
        Task UploadProfileImage(string userId, ByteArrayPart bytePart);
    }
}
