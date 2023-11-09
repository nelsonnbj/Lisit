using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTheLastBugSpa.Data.Procedure
{
    public class ServicesByPeopleDescription
    {
        public int Id{ get; set; }
        public string FullName{ get; set; }
        public string Services { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
