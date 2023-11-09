
using System.Linq;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.IRepository
{
    public interface IRegionesRepository : IGenericRepository<Region>
    {
        Task<IQueryable<Region>> GetRegion();
    }
}
