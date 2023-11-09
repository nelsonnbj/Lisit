using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTheLastBugSpa.Data.Entity
{
   public partial class People
    {
        public People()
        {
            ServicesByPeoples = new HashSet<ServicesByPeople>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NoDocument { get; set; }
        public int ComunaId { get; set; }


        public virtual Comuna Comuna { get; set; }
        public virtual ICollection<ServicesByPeople> ServicesByPeoples { get; set; }

    }
}
