using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Repository
{
    public class RefreshTokensRepository:GenericRepository<RefreshToken>
    {
        private readonly AplicationDataContext context;
        public RefreshTokensRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
