using Portfolio.Profile.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Profile.API.Interfaces
{
    public interface IProfileRepository
    {
        Task AddAsync(Profiles profile);

        Task<List<Profiles>> GetAllAsync();

        Task<Profiles?> GetByIdAsync(Guid id);

        Task UpdateAsync(Profiles profile);

        Task DeleteAsync(Profiles profile);

        Task SaveChangesAsync();
    }
}
