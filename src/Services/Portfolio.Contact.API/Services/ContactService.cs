using Portfolio.Contact.API.Entities;
using Portfolio.Contact.API.Interfaces;
using Portfolio.Shared.Contracts.DTO;
using Portfolio.Shared.Contracts.Requests;

namespace Portfolio.Contact.API.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(
            IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContactMessageDto> CreateAsync(
            CreateContactMessageRequest request)
        {
            var entity = new ContactMessage
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Subject = request.Subject,
                Message = request.Message,
                IsRead = false
            };

            await _repository.AddAsync(entity);

            await _repository.SaveChangesAsync();

            return Map(entity);
        }

        public async Task<List<ContactMessageDto>> GetAllAsync()
        {
            var messages =
                await _repository.GetAllAsync();

            return messages
                .Select(Map)
                .ToList();
        }

        public async Task<ContactMessageDto?> GetByIdAsync(Guid id)
        {
            var message =
                await _repository.GetByIdAsync(id);

            return message == null
                ? null
                : Map(message);
        }

        private static ContactMessageDto Map(
            ContactMessage entity)
        {
            return new ContactMessageDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Subject = entity.Subject,
                Message = entity.Message,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
