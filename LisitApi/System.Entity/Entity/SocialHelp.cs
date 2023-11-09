using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTheLastBugSpa.Data.Entity
{
    public class SocialHelp
    {
        public SocialHelp()
        {
            ServicesByPeople = new HashSet<ServicesByPeople>();
        }
        public int Id { get; set; }
        public string Services { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ServicesByPeople> ServicesByPeople { get; set; }
    }
}
