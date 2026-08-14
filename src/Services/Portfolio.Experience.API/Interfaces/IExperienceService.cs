using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;
using Portfolio.Shared.Contracts.Responses;

namespace Portfolio.Experience.API.Interfaces
{
    public interface IExperienceService
    {
        Task<IEnumerable<ExperienceDto>> GetAllAsync();

        Task<ExperienceDto?> GetByIdAsync(Guid id);

        Task<ApiResponse<ExperienceDto>> CreateAsync(
            CreateExperienceRequest request);
        Task<ApiResponse<ExperienceDto>> UpdateAsync(
           Guid id,
           UpdateExperienceRequest request);

        Task<bool> DeleteAsync(Guid id);
    }
}
