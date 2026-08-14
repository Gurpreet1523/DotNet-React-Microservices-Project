using Portfolio.Experience.API.Entities;
using Portfolio.Experience.API.Interfaces;
using Portfolio.Experience.API.Repositories;
using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;
using Portfolio.Shared.Contracts.Responses;

namespace Portfolio.Experience.API.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperiencesRepository _repository;
        public ExperienceService(
            IExperiencesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExperienceDto>> GetAllAsync()
        {
            var experiences = await _repository.GetAllAsync();

            return experiences.Select(MapToDto);
        }

        public async Task<ExperienceDto?> GetByIdAsync(Guid id)
        {
            var experience = await _repository.GetByIdAsync(id);

            return experience == null
                ? null
                : MapToDto(experience);
        }

        public async Task<ApiResponse<ExperienceDto>> CreateAsync(
            CreateExperienceRequest request)
        {
            var experience = new Experiencee
            {
                Id = Guid.NewGuid(),
                JobTitle = request.JobTitle,
                Company = request.Company,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsCurrent = request.IsCurrent,
                Description = request.Description
            };

            await _repository.CreateAsync(experience);

            return new ApiResponse<ExperienceDto>
            {
                Success = true,
                Message = "Experience created successfully.",
                Data = MapToDto(experience)
            };
        }

        public async Task<ApiResponse<ExperienceDto>> UpdateAsync(
            Guid id,
            UpdateExperienceRequest request)
        {
            var experience = await _repository.GetByIdAsync(id);

            if (experience == null)
            {
                return new ApiResponse<ExperienceDto>
                {
                    Success = false,
                    Message = "Experience not found."
                };
            }

            experience.JobTitle = request.JobTitle;
            experience.Company = request.Company;
            experience.Location = request.Location;
            experience.StartDate = request.StartDate;
            experience.EndDate = request.EndDate;
            experience.IsCurrent = request.IsCurrent;
            experience.Description = request.Description;

            await _repository.UpdateAsync(experience);

            return new ApiResponse<ExperienceDto>
            {
                Success = true,
                Message = "Experience updated successfully.",
                Data = MapToDto(experience)
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static ExperienceDto MapToDto(
            Experiencee experience)
        {
            return new ExperienceDto
            {
                Id = experience.Id,
                JobTitle = experience.JobTitle,
                Company = experience.Company,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent,
                Description = experience.Description
            };
        }
    }
}
