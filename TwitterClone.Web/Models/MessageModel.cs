using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Web.Models
{
    public class MessageModel
    {
        [Required(ErrorMessage = "Log in to post a message")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Your message can't be empty")]
        public string Text { get; set; }
    }
}
