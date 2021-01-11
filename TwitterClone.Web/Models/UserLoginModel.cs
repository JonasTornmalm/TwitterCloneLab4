using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Web.Models
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 7)]
        public string Password { get; set; }
    }
}
