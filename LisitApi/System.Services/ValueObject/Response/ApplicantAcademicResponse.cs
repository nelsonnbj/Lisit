using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ApplicantAcademicResponse
    {
        public string HighschoolHighSchool { get; set; }
        public bool IsInCollege { get; set; }
        public Guid? UniversityId { get; set; }
        public string UniversityName { get; set; }
        public Guid? CareerId { get; set; }
        public string CareerName { get; set; }
        public string Currentcycle { get; set; }
        public bool? FinishTheCareer { get; set; }
        public bool IsGraduate { get; set; }
        public bool AwardedScholarshipMcyt { get; set; }
        public string WhichCenterStudy { get; set; }
        public bool ReadSpeakWriteEnglish { get; set; }
        public bool Interestedstudyingabroad { get; set; }
        public string LinkVideo { get; set; }
        public string ScheduleStudy { get; set; }
    }
}
