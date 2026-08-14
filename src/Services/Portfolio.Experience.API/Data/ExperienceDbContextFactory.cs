using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Experience.API.Data
{
    public class ExperienceDbContextFactory : IDesignTimeDbContextFactory<ExperienceDbContext>
    {
        public ExperienceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<ExperienceDbContext>();

            var connectionString =
                "Server=localhost;Database=PortfolioDb;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new ExperienceDbContext(optionsBuilder.Options);
        }
    }
    }
