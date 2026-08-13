using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Contact.API.Interfaces
{
    public interface IContactService
    {
        Task<ContactMessageDto> CreateAsync(CreateContactMessageRequest request);

        Task<List<ContactMessageDto>> GetAllAsync();

        Task<ContactMessageDto?> GetByIdAsync(Guid id);
    }
}
