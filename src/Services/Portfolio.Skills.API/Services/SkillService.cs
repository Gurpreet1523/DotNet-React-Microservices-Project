using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;
using Portfolio.Skills.API.Entities;
using Portfolio.Skills.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Skills.API.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;

        public SkillService(
            ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<SkillDto> CreateAsync(
            CreateSkillRequest request)
        {
            var skill = new Skill
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Category = request.Category,
                ExperienceYears = request.ExperienceYears,
                DisplayOrder = request.DisplayOrder
            };

            await _repository.AddAsync(skill);
            await _repository.SaveChangesAsync();

            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Category = skill.Category,
                ExperienceYears = skill.ExperienceYears,
                DisplayOrder = skill.DisplayOrder
            };
        }

        public async Task<List<SkillDto>> GetAllAsync()
        {
            var skills = await _repository.GetAllAsync();

            return skills.Select(x => new SkillDto
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                ExperienceYears = x.ExperienceYears,
                DisplayOrder = x.DisplayOrder
            }).ToList();
        }

        public async Task<SkillDto?> GetByIdAsync(Guid id)
        {
            var skill = await _repository.GetByIdAsync(id);

            if (skill == null)
                return null;

            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Category = skill.Category,
                ExperienceYears = skill.ExperienceYears,
                DisplayOrder = skill.DisplayOrder
            };
        }

        public async Task<SkillDto?> UpdateAsync(
            Guid id,
            UpdateSkillRequest request)
        {
            var skill = await _repository.GetByIdAsync(id);

            if (skill == null)
                return null;

            skill.Name = request.Name;
            skill.Category = request.Category;
            skill.ExperienceYears = request.ExperienceYears;
            skill.DisplayOrder = request.DisplayOrder;

            await _repository.UpdateAsync(skill);
            await _repository.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var skill = await _repository.GetByIdAsync(id);

            if (skill == null)
                return false;

            await _repository.DeleteAsync(skill);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
