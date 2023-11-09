using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ApplicantWorkingDataResponse
    {
        public bool Isworking { get; set; }
        public Double? SalaryPerMonth { get; set; }
        public bool KnowledgeComputer { get; set; }
        public string CompanyInstituteWhereWorks { get; set; }
        public string Position { get; set; }
        public bool DispositionMobilize { get; set; }
        public Guid? AreasIwouldLikeWork { get; set; }
        public int? TimeToDedicateToStudyEnglish { get; set; }
        public Guid? KnowledgeLanguageEnglish { get; set; }
    }
}
