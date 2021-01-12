using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Web.DTOs;
using TwitterClone.Web.Models;

namespace TwitterClone.Web.ViewModels
{
    public class MessagesViewModel
    {
        public IEnumerable<MessageDTO> Messages { get; set; }
        public MessageModel SendMessage { get; set; }
    }
}
