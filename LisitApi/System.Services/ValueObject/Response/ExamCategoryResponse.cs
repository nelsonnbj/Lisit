using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ExamCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Narrative { get; set; }
        public string Book { get; set; }
    }
}
