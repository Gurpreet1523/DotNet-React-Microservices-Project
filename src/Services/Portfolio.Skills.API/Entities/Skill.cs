namespace Portfolio.Skills.API.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public int ExperienceYears { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
