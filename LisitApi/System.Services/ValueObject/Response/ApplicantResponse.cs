using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ApplicantResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime? BirthDates { get; set; }
        public Guid? ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public Guid? NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; }
        public string Address { get; set; }
        public string CelPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string LinkVideo { get; set; }
        public string Tanda { get; set; }
    }
}
