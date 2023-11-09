using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ApplicantDetailsResponse
    {
        public Guid Id { get; set; }
        public Guid AnnouncementId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime BirthDates { get; set; }
        public string Gender { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Providence { get; set; }
        public string Sector { get; set; }
        public string Address { get; set; }
        public string IdentificationCard { get; set; }
        public string IdentificationCardURL { get; set; }
        public string CertificationURL { get; set; }
        public string Highschool { get; set; }
        public bool IsInCollege { get; set; }
        public string University { get; set; }
        public string Currentcycle { get; set; }
        public string Career { get; set; }
        public int? YearOfGraduation { get; set; }
        public string WhichCenterStudy { get; set; }
        public bool FinishTheCareer { get; set; }
        public string Tanda { get; set; }
        public Guid TandaId { get; set; }
        public string Email { get; set; }
        public string LinkVideo { get; set; }
        public string Centro { get; set; }
        public Guid DependenceId { get; set; }
        public Guid InstitutionId { get; set; }
        public string State { get; set; }
    }
}
