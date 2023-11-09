using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ApplicantInfoResponse
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Lenguage { get; set; }
        public string Anouncement { get; set; }
        public string Status { get; set; }
        public string UrlDocumentId { get; set; }
        public string UrlCertification { get; set; }
    }
}
