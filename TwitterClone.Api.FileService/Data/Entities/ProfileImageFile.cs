using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Api.FileService.Data.Entities
{
    public class ProfileImageFile
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public byte[] ProfileImageBytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
