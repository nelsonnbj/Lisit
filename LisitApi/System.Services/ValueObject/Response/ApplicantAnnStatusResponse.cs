using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ApplicantAnnStatusResponse
    {
        public string Email { get; set; }
        public string DocumentId { get; set; }
        public bool EmailConfirmation { get; set; }
        public bool FirstLogin { get; set; }
        public bool FormCompleted { get; set; }
    }
}
