using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Requests
{
    public class CreateProfileRequest
    {
        public string FullName { get; set; }

        public string Title { get; set; }

        public string Bio { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string LinkedInUrl { get; set; }

        public string GitHubUrl { get; set; }

        public string ResumeUrl { get; set; }
    }
}
