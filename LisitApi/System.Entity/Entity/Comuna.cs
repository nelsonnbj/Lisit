using System;
using System.Collections.Generic;

namespace SystemTheLastBugSpa.Data.Entity
{
    public partial class Comuna
    {
        public Comuna()
        {
            People = new HashSet<People>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<People> People { get; set; }

    }
}
