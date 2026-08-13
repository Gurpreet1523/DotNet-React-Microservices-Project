using Portfolio.Skills.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Portfolio.Skills.API.Data
{
    public class SkillsDbContext : DbContext
    {
        public SkillsDbContext(
            DbContextOptions<SkillsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }
    }
}
