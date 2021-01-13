using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Web.DTOs
{
    public class ProfileImageFileDTO
    {
        public string UserId { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
