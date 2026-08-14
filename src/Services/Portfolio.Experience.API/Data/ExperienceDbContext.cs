using Microsoft.EntityFrameworkCore;
using Portfolio.Experience.API.Entities;

namespace Portfolio.Experience.API.Data
{
    public class ExperienceDbContext : DbContext
    {
        public ExperienceDbContext(
            DbContextOptions<ExperienceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Experiencee> Experiencees { get; set; }

       
    }
}
