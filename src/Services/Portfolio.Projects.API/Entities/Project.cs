namespace Portfolio.Projects.API.Entities
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Technologies { get; set; }

        public string GitHubUrl { get; set; }

        public string LiveUrl { get; set; }

        public string ImageUrl { get; set; }

        public bool Featured { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
