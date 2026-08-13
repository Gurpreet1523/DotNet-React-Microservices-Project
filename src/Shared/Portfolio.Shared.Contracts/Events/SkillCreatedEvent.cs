using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Events
{
    public class SkillCreatedEvent
    {
        public Guid SkillId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
