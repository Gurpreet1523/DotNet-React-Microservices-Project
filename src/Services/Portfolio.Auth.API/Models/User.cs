namespace Portfolio.Auth.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}

