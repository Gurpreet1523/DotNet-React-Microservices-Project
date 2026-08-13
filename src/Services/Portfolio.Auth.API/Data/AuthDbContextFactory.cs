using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Portfolio.Auth.API.Models;

namespace Portfolio.Auth.API.Data
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=PortfolioAuthDb;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
