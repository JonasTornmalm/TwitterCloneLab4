using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterClone.Web.Models;

namespace TwitterClone.Web.RESTClients
{
    public class UserServiceAPI : IUserServiceAPI
    {
        private IUserServiceAPI _restClient;

        public UserServiceAPI(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("UserServiceAPI");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/");
            _restClient = RestService.For<IUserServiceAPI>(httpClient);
        }


        //public async Task<List<User>> GetUsers()
        //{
        //    return await _restClient.GetUsers();
        //}
        public async Task<string> GetUsers()
        {
            return await _restClient.GetUsers();
        }
        public async Task<User> GetUserById([AliasAs("id")] string customerId)
        {
            try
            {
                return await _restClient.GetUserById(customerId);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
