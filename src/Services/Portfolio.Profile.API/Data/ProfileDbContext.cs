using Microsoft.EntityFrameworkCore;
using Portfolio.Profile.API.Entities;
using System.Collections.Generic;

namespace Portfolio.Profile.API.Data
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext(
            DbContextOptions<ProfileDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profiles> Profiles { get; set; }
    }
}
