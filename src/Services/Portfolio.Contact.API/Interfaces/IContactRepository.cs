using Portfolio.Contact.API.Entities;

namespace Portfolio.Contact.API.Interfaces
{
    public interface IContactRepository
    {
        Task<List<ContactMessage>> GetAllAsync();

        Task<ContactMessage?> GetByIdAsync(Guid id);

        Task AddAsync(ContactMessage message);

        Task SaveChangesAsync();
    }
}
