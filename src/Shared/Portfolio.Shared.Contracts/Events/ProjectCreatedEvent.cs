using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Events
{
    public class ProjectCreatedEvent
    {
        public Guid ProjectId { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
