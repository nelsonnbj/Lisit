

using System.Linq;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
       Task<IQueryable<Country>> GetCaontry();
    }
}
