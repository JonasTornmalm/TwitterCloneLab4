using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterClone.Web.Models;
using System.Net;
using TwitterClone.Web.DTOs;

namespace TwitterClone.Web.RESTClients
{
    public class FileServiceAPI : IFileServiceAPI
    {
        private IFileServiceAPI _restClient;

        public FileServiceAPI(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("FileServiceAPI");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
            _restClient = RestService.For<IFileServiceAPI>(httpClient);
        }

        public async Task<ByteArrayPart> GetUserProfileImage(string userId)
        {
            try
            {
                return await _restClient.GetUserProfileImage(userId);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }

        public async Task<HttpResponseMessage> UploadProfileImage(string userId, ByteArrayPart bytePart)
        {
            return await _restClient.UploadProfileImage(userId, bytePart);
        }

        public async Task<HttpResponseMessage> DeleteProfileImage(string userId)
        {
            return await _restClient.DeleteProfileImage(userId);
        }
    }
}
