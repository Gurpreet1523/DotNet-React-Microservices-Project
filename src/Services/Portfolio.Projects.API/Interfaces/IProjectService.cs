using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Projects.API.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateAsync(
        CreateProjectRequest request);

        Task<List<ProjectDto>> GetAllAsync();

        Task<ProjectDto?> GetByIdAsync(Guid id);

        Task<ProjectDto?> UpdateAsync(
            Guid id,
            UpdateProjectRequest request);

        Task<bool> DeleteAsync(Guid id);
    }
}
