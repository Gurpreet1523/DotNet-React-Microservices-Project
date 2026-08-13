using Portfolio.Profile.API.Data;
using Portfolio.Profile.API.Entities;
using Microsoft.EntityFrameworkCore;
using Portfolio.Profile.API.Interfaces;

namespace Portfolio.Profile.API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileDbContext _context;

        public ProfileRepository(ProfileDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Profiles profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public async Task<List<Profiles>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profiles?> GetByIdAsync(Guid id)
        {
            return await _context.Profiles
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Profiles profile)
        {
            _context.Profiles.Update(profile);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Profiles profile)
        {
            _context.Profiles.Remove(profile);

            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
