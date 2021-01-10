using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Web.Models
{
    public class User
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Phonenumber")]
        public string TelephoneNumber { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [EmailAddress()]
        public string EmailAddress { get; set; }
    }
}
