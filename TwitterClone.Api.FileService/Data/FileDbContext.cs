using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Api.FileService.Data.Entities;

namespace TwitterClone.Api.FileService.Data
{
    public class FileDbContext : DbContext
    {
        public DbSet<ProfileImageFile> ProfileImageFiles { get; set; }
        public FileDbContext(DbContextOptions<FileDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
