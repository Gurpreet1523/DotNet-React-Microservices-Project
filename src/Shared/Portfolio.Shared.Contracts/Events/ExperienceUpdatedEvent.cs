using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Events
{
    public class ExperienceUpdatedEvent
    {
        public Guid ExperienceId { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
