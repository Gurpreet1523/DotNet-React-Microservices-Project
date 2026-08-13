using NPOI.SS.Formula.Functions;
using Portfolio.Skills.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Skills.API.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();

        Task<Skill?> GetByIdAsync(Guid id);
        Task AddAsync(Skill skill);

        Task UpdateAsync(Skill skill);

        Task DeleteAsync(Skill skill);

        Task SaveChangesAsync();
    }
}
