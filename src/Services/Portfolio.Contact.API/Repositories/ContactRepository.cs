using Microsoft.EntityFrameworkCore;
using Portfolio.Contact.API.Data;
using Portfolio.Contact.API.Entities;
using Portfolio.Contact.API.Interfaces;

namespace Portfolio.Contact.API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;

        public ContactRepository(
            ContactDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactMessage>> GetAllAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactMessage?> GetByIdAsync(Guid id)
        {
            return await _context.ContactMessages
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ContactMessage message)
        {
            await _context.ContactMessages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
