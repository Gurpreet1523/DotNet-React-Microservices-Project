namespace Portfolio.Profile.API.Entities
{
    public class Profiles
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }

        public string Bio { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string LinkedInUrl { get; set; }

        public string GitHubUrl { get; set; }

        public string ResumeUrl { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
