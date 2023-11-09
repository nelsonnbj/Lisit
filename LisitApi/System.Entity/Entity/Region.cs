using System;
using System.Collections.Generic;

namespace SystemTheLastBugSpa.Data.Entity
{
    public partial class Region
    {
        public Region()
        {
            Comunas = new HashSet<Comuna>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public short PaisId { get; set; }

        public virtual Country Pais { get; set; }
        public virtual ICollection<Comuna> Comunas { get; set; }
    }
}
