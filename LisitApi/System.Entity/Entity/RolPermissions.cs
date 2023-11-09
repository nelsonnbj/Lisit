using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTheLastBugSpa.Data.Entity
{
    public class RolPermissions: BaseEntity
    {
        public Guid RolId { get; set; }
        public Guid PermissionsId { get; set; }
    }
}
