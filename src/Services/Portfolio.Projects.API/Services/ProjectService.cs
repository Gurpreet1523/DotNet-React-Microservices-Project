using Portfolio.Projects.API.Entities;
using Portfolio.Projects.API.Interfaces;
using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Projects.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(
            IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProjectDto> CreateAsync(
            CreateProjectRequest request)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Technologies = request.Technologies,
                GitHubUrl = request.GitHubUrl,
                LiveUrl = request.LiveUrl,
                ImageUrl = request.ImageUrl,
                Featured = request.Featured
            };

            await _repository.AddAsync(project);
            await _repository.SaveChangesAsync();

            return Map(project);
        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();

            return projects.Select(Map).ToList();
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var project = await _repository.GetByIdAsync(id);

            return project == null
                ? null
                : Map(project);
        }

        public async Task<ProjectDto?> UpdateAsync(
            Guid id,
            UpdateProjectRequest request)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return null;

            project.Title = request.Title;
            project.Description = request.Description;
            project.Technologies = request.Technologies;
            project.GitHubUrl = request.GitHubUrl;
            project.LiveUrl = request.LiveUrl;
            project.ImageUrl = request.ImageUrl;
            project.Featured = request.Featured;
            project.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(project);
            await _repository.SaveChangesAsync();

            return Map(project);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return false;

            await _repository.DeleteAsync(project);
            await _repository.SaveChangesAsync();

            return true;
        }

        private static ProjectDto Map(Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Technologies = project.Technologies,
                GitHubUrl = project.GitHubUrl,
                LiveUrl = project.LiveUrl,
                ImageUrl = project.ImageUrl,
                Featured = project.Featured
            };
        }
    }
}
