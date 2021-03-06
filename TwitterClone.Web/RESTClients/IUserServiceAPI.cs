﻿using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.Models;

namespace TwitterClone.Web.RESTClients
{
    public interface IUserServiceAPI
    {
        [Get("/userregister")]
        Task<List<UserDTO>> GetUsers();

        [Post("/userregister")]
        Task RegisterUser(UserDTO registerUser);

        [Delete("/userregister")]
        Task<HttpResponseMessage> DeleteUser([Body(BodySerializationMethod.Serialized)] DeleteUserDTO deleteUserDto);

        [Get("/userlogin")]
        Task<UserDTO> TryLogin([Body(BodySerializationMethod.Serialized)] UserDTO loginUser);
    }
}
