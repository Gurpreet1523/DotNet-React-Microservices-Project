using Microsoft.EntityFrameworkCore;
using Portfolio.Experience.API.Data;
using Portfolio.Experience.API.Entities;
using Portfolio.Experience.API.Interfaces;

namespace Portfolio.Experience.API.Repositories
{
    public class ExperiencesRepository : IExperiencesRepository
    {
        private readonly ExperienceDbContext _context;

        public ExperiencesRepository(ExperienceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Experiencee>> GetAllAsync()
        {
            return await _context.Experiencees
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        public async Task<Experiencee?> GetByIdAsync(Guid id)
        {
            return await _context.Experiencees
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Experiencee> CreateAsync(
            Experiencee experience)
        {
            _context.Experiencees.Add(experience);

            await _context.SaveChangesAsync();

            return experience;
        }

        public async Task<bool> UpdateAsync(
            Experiencee experience)
        {
            var existing = await _context.Experiencees
                .FirstOrDefaultAsync(x => x.Id == experience.Id);

            if (existing == null)
                return false;

            existing.JobTitle = experience.JobTitle;
            existing.Company = experience.Company;
            existing.Location = experience.Location;
            existing.StartDate = experience.StartDate;
            existing.EndDate = experience.EndDate;
            existing.IsCurrent = experience.IsCurrent;
            existing.Description = experience.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var experience = await _context.Experiencees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (experience == null)
                return false;

            _context.Experiencees.Remove(experience);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
