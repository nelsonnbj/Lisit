using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTheLastBugSpa.Data.Entity
{
    public  class ServicesByPeople
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public int SocialHelpId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual People People { get; set; }
        public virtual SocialHelp SocialHelp { get; set; }
    }
}
