using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class AnnouncementRequestResponse
    {
        public Guid Id { get; set; }
        public Guid AnnouncementId { get; set; }
        public Guid InsitutionId { get; set; }
        public Guid SatetId { get; set; }
        public Guid DependenceId { get; set; }
        public string AnnouncementName { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdentityCard { get; set; }
        public string Status { get; set; }
        public string Centro { get; set; }
        public string Tanda { get; set; }
        public bool ShowStatus { get; set; }
        public string Dependence { get; set; }
        public string Institution { get; set; }
        public string State { get; set; }
        public string Age { get; set; }
    }
}
