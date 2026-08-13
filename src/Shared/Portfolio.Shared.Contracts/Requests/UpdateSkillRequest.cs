using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Requests
{
    public class UpdateSkillRequest
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public int ExperienceYears { get; set; }

        public int DisplayOrder { get; set; }
    }
}
