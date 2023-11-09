using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ApplicantEmergencyContactResponse
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public Guid? RelationshipId { get; set; }
        public string RelationshipName { get; set; }
    }
}
