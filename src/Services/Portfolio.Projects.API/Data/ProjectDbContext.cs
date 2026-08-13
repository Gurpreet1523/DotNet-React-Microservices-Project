using Microsoft.EntityFrameworkCore;
using Portfolio.Projects.API.Entities;
using System.Collections.Generic;

namespace Portfolio.Projects.API.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(
            DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
    }
}
