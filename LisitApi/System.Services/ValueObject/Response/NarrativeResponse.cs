using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class NarrativeResponse
    {
        public Guid Id { get; set; }
        public string LanguageName { get; set; }
        public string Name { get; set; }
    }
}
