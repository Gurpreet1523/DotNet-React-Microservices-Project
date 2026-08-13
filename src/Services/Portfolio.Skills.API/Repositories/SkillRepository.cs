using Microsoft.EntityFrameworkCore;
using Portfolio.Skills.API.Data;
using Portfolio.Skills.API.Entities;
using Portfolio.Skills.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Skills.API.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly SkillsDbContext _context;

        public SkillRepository(
            SkillsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetAllAsync()
        {
            return await _context.Skills
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<Skill?> GetByIdAsync(Guid id)
        {
            return await _context.Skills
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
        }

        public async Task UpdateAsync(Skill skill)
        {
            _context.Skills.Update(skill);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Skill skill)
        {
            _context.Skills.Remove(skill);

            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
