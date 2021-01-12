using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;

namespace TwitterClone.Web.RESTClients
{
    public class MessagingServiceAPI : IMessagingServiceAPI
    {
        private IMessagingServiceAPI _restClient;

        public MessagingServiceAPI(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("MessagingServiceAPI");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
            _restClient = RestService.For<IMessagingServiceAPI>(httpClient);
        }
        public async Task<List<MessageDTO>> GetMessages()
        {
            return await _restClient.GetMessages();
        }
        public async Task SendMessage(MessageDTO message)
        {
            await _restClient.SendMessage(message);
        }
    }
}
