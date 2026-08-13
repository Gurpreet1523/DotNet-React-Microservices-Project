using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace Portfolio.Auth.API.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
    : base(options){ }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId);
        }
    }
}
