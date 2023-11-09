using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class BookRequestResponse
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Observation { get; set; }
        public string RequestedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime CreateBy { get; set; }
        public List<BookRequestDetailsResponse> BookRequestDetails { get; set; }
    }
    public class BookRequestDetailsResponse
    {
        public Guid Id { get; set; }
        public Guid BookRequestId { get; set; }
        public string Language { get; set; }
        public string Book { get; set; }
        public string Typology { get; set; }
        public string Level { get; set; }
        public int QuantityRequest { get; set; }
        public int QuantityAfford { get; set; }
    }
}
