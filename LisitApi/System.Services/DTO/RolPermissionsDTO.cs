using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.DTO
{
    public class RolPermissionsDTO
    {
        public Guid Id { get; set; }
        public Guid RolId { get; set; }
        public Guid PermissionsId { get; set; }
    }
}
