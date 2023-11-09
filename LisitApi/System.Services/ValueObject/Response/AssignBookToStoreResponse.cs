using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class AssignBookToStoreResponse
    {
        public Guid Id { get; set; }
        public string Store { get; set; }
        public string Book { get; set; }
        public string Typology { get; set; }
        public string Observation { get; set; }
        public int Count { get; set; }
        public DateTime CreateBy { get; set; }
    }
}
