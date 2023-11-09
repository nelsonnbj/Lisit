using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class DischargeBookRequestResponse
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string RequestedBy { get; set; }
        public string ApprovedBy { get; set; }
        public int DischargeBookCount { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
