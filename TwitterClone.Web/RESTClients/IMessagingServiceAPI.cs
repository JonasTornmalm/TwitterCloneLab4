using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;

namespace TwitterClone.Web.RESTClients
{
    public interface IMessagingServiceAPI
    {
        [Get("/messaging")]
        Task<List<MessageDTO>> GetMessages();

        [Post("/messaging")]
        Task SendMessage(MessageDTO message);
    }
}
