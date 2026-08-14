using Portfolio.Experience.API.Entities;

namespace Portfolio.Experience.API.Interfaces
{
    public interface IExperiencesRepository
    {
        Task<IEnumerable<Experiencee>> GetAllAsync();

        Task<Experiencee?> GetByIdAsync(Guid id);

        Task<Experiencee> CreateAsync(Experiencee experience);

        Task<bool> UpdateAsync(Experiencee experience);

        Task<bool> DeleteAsync(Guid id);
    }
}
