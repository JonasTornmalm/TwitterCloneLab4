using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Web.Models
{
    public class ProfileImageFileModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Base64Image { get; set; }

        public string MimeType { get; set; }

        public bool HasLogo { get; set; }
    }
}
