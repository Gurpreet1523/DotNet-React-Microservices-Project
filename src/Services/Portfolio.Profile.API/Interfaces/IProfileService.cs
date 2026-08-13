using Portfolio.Shared.Contracts.Requests;
using Portfolio.Shared.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Profile.API.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileResponse> CreateAsync(
       CreateProfileRequest request);

        Task<IEnumerable<ProfileResponse>> GetAllAsync();

        Task<ProfileResponse?> GetByIdAsync(Guid id);

        Task<ProfileResponse?> UpdateAsync(
            Guid id,
            UpdateProfileRequest request);

        Task<bool> DeleteAsync(Guid id);
    }
}
