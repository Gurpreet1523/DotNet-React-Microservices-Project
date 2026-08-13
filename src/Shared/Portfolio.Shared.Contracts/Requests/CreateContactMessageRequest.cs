using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Contracts.Requests
{
    public class CreateContactMessageRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
