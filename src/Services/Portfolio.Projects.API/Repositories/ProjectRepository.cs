using Microsoft.EntityFrameworkCore;
using Portfolio.Projects.API.Data;
using Portfolio.Projects.API.Entities;
using Portfolio.Projects.API.Interfaces;

namespace Portfolio.Projects.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectDbContext _context;

        public ProjectRepository(
            ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Project project)
        {
            _context.Projects.Remove(project);

            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
