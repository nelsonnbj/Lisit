using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class CenterResponse
    {
        public string Centro { get => InstitutionAcronym + DependencesAcronym;  }
        public string Tanda { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid DependenceId { get; set; }
        public string DependencesAcronym { get; set; }
        public Guid InstitutionId { get; set; }
        public string InstitutionAcronym { get; set; }
        
    }
}
