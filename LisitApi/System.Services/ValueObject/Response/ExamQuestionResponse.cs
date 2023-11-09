using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.ValueObject.Response
{
    public class ExamQuestionResponse
    {
        public Guid Id { get; set; }
        public string Language { get; set; }
        public string Book { get; set; }
        public string Level { get; set; }
        public string Narrative { get; set; }
        public string Name { get; set; }
        public string TypeAnswer { get; set; }
        public string UrlFile { get; set; }
        public string NameFile { get; set; }
    }
}
