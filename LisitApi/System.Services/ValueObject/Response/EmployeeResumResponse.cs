using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class EmployeeResumResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Identification { get; set; }
        public DateTime? BirthDates { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Tanda { get; set; }
        public string Email { get; set; }
        public float Salary { get; set; }
        public string Position { get; set; }
        public string LaborUnitDepartment { get; set; }
    }
}
