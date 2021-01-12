using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Api.MessagingService.Data.Entities;

namespace TwitterClone.Api.MessagingService.Data
{
    public class MessageDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
