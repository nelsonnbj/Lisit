using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Infrastructure.IRepository;
using System.Linq;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Repository
{
    public class RegionRepository : GenericRepository<Region>, IRegionesRepository
    {
        private readonly AplicationDataContext context;
        public RegionRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IQueryable<Region>> GetRegion()
        {
            return context.Region
                .Include(x => x.Comunas)
                .Include(s=> s.Pais);
        }
    }
}
