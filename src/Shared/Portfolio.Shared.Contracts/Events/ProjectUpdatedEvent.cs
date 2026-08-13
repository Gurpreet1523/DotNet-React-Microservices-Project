using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Events
{
    public class ProjectUpdatedEvent
    {
        public Guid ProjectId { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
