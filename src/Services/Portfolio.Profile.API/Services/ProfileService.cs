using Portfolio.Profile.API.Repositories;
using Portfolio.Shared.Contracts.Requests;
using Portfolio.Shared.Contracts.Responses;
using Portfolio.Profile.API.Entities;
using Portfolio.Profile.API.Interfaces;

namespace Portfolio.Profile.API.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;

        public ProfileService(
            IProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProfileResponse> CreateAsync(
            CreateProfileRequest request)
        {
            var profile = new Profiles
            {
                Id = Guid.NewGuid(),

                FullName = request.FullName,

                Title = request.Title,

                Bio = request.Bio,

                Email = request.Email,

                Phone = request.Phone,

                LinkedInUrl = request.LinkedInUrl,

                GitHubUrl = request.GitHubUrl,

                ResumeUrl = request.ResumeUrl
            };

            await _repository.AddAsync(profile);

            await _repository.SaveChangesAsync();

            return new ProfileResponse
            {
                Id = profile.Id,

                FullName = profile.FullName,

                Title = profile.Title,

                Bio = profile.Bio,

                Email = profile.Email,

                Phone = profile.Phone,

                LinkedInUrl = profile.LinkedInUrl,

                GitHubUrl = profile.GitHubUrl,

                ResumeUrl = profile.ResumeUrl
            };
        }

        public async Task<IEnumerable<ProfileResponse>> GetAllAsync()
        {
            var profiles = await _repository.GetAllAsync();

            return profiles.Select(x => new ProfileResponse
            {
                Id = x.Id,
                FullName = x.FullName,
                Title = x.Title,
                Bio = x.Bio,
                Email = x.Email,
                Phone = x.Phone,
                LinkedInUrl = x.LinkedInUrl,
                GitHubUrl = x.GitHubUrl,
                ResumeUrl = x.ResumeUrl
            });
        }

        public async Task<ProfileResponse?> GetByIdAsync(Guid id)
        {
            var profile = await _repository.GetByIdAsync(id);

            if (profile == null)
                return null;

            return new ProfileResponse
            {
                Id = profile.Id,
                FullName = profile.FullName,
                Title = profile.Title,
                Bio = profile.Bio,
                Email = profile.Email,
                Phone = profile.Phone,
                LinkedInUrl = profile.LinkedInUrl,
                GitHubUrl = profile.GitHubUrl,
                ResumeUrl = profile.ResumeUrl
            };
        }

        public async Task<ProfileResponse?> UpdateAsync(
            Guid id,
            UpdateProfileRequest request)
        {
            var profile = await _repository.GetByIdAsync(id);

            if (profile == null)
                return null;

            profile.FullName = request.FullName;
            profile.Title = request.Title;
            profile.Bio = request.Bio;
            profile.Phone = request.Phone;
            profile.LinkedInUrl = request.LinkedInUrl;
            profile.GitHubUrl = request.GitHubUrl;
            profile.ResumeUrl = request.ResumeUrl;
            profile.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(profile);

            await _repository.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var profile = await _repository.GetByIdAsync(id);

            if (profile == null)
                return false;

            await _repository.DeleteAsync(profile);

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
