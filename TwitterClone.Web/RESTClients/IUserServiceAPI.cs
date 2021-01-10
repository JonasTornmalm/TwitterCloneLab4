using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.Models;

namespace TwitterClone.Web.RESTClients
{
    public interface IUserServiceAPI
    {
        [Get("/user")]
        Task<List<User>> GetUsers();

        [Get("/user/{id}")]
        Task<User> GetUserById([AliasAs("id")] string userId);
    }
}
