using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.Models;

namespace TwitterClone.Web.RESTClients
{
    public interface IUserServiceAPI
    {
        [Get("/user")]
        Task<List<User>> GetUsers();

        [Get("/user/{id}")]
        Task<UserModel> GetUserById([AliasAs("id")] string userId);

        [Post("/user")]
        Task RegisterCustomer(User registerUser);
    }
}
