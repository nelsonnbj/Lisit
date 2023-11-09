using System.Data;
using System.Infrastructure.IRepository;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Repository
{
    public class PeopleRepository : GenericRepository<People>, IPeopleRepository
    {
        private readonly AplicationDataContext context;
        public PeopleRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
