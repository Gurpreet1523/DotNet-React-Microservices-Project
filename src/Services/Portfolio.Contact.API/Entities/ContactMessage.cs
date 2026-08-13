namespace Portfolio.Contact.API.Entities
{
    public class ContactMessage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
