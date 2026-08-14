using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Events
{
    public class ExperienceCreatedEvent
    {

        public Guid ExperienceId {  get; set; }
        public string JobTitle {  get; set; }
        public string Company {  get; set; }
    }
}
