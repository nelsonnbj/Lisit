using System;
using System.Collections.Generic;
using System.Infrastructure.IRepository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;
using SystemTheLastBugSpa.Data;

namespace System.Infrastructure.Repository
{
    public class RolPermissionsRepository : GenericRepository<RolPermissions>, IRolPermissionsRepository
    {
        private readonly AplicationDataContext context;
        public RolPermissionsRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
