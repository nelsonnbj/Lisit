using System;
using System.Collections.Generic;

namespace SystemTheLastBugSpa.Data.Entity
{
    public partial class Country
    {
        public Country()
        {
            Regions = new HashSet<Region>();
        }

        public short Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
