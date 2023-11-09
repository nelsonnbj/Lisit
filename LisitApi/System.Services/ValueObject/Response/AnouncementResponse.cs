using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class AnouncementResponse
    {
        public Guid Id { get; set; }
        public string Convocatoria { get; set; }
        public string State { get; set; }
        public Guid StatusId { get; set; }
        public string Language { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int Age { get; set; }
    }
}
