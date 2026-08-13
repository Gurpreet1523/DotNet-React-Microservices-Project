using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Skills.API.Interfaces
{
    public interface ISkillService
    {
        Task<SkillDto> CreateAsync(CreateSkillRequest request);

        Task<List<SkillDto>> GetAllAsync();

        Task<SkillDto?> GetByIdAsync(Guid id);

        Task<SkillDto?> UpdateAsync(
            Guid id,
            UpdateSkillRequest request);

        Task<bool> DeleteAsync(Guid id);
    }
}
