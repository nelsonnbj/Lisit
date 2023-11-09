using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Infrastructure.IRepository;
using System.Linq;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly AplicationDataContext context;
        public CountryRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IQueryable<Country>> GetCaontry()
        {
            return context.Country.Include(x => x.Regions);
        }

    }
}
