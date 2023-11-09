using System.Data;
using System.Infrastructure.IRepository;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;


namespace System.Infrastructure.Repository
{
    public class ComunaRepository : GenericRepository<Comuna>, IComunaRepository
    {
        private readonly AplicationDataContext context;
        public ComunaRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
