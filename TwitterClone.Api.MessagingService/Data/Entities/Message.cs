using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Api.MessagingService.Data.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
    }
}
