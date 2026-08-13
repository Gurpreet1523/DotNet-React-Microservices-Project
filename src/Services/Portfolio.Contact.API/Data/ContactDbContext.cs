using Microsoft.EntityFrameworkCore;
using Portfolio.Contact.API.Entities;
using System.Collections.Generic;

namespace Portfolio.Contact.API.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(
            DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
