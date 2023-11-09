using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class StoreResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Institution { get; set; }
        public Guid? InstitutionId { get; set; }
        public string Dependences { get; set; }
        public Guid? DependenceId { get; set; }
    }
}
