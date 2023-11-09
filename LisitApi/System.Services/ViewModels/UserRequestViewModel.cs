using System.ComponentModel.DataAnnotations;

namespace System.Infrastructure.ViewModels
{
    public class UserRequestViewModel
    {
        public string PlaceOfBirth { get; set; }
        public DateTime? BirthDates { get; set; }
        public int? AnnouncementId { get; set; }
        public int ProvinceId { get; set; }
        public int? NeighborhoodId { get; set; }
        public int? TypeRol { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string CelPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public DateTime? Date { get; set; }
    }
}
