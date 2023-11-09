using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.IRepository
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        void Delete(int id);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entity);
        Task<T> Create(T entity);
        Task<T> CreateRange(IEnumerable<T> entity);
        Task<T> Update(T entity);
        Task<List<T>> UpdateRange(List<T> entity);
        IQueryable<T> GetQueryable();
        Task<List<T>> Get();
        IQueryable<T> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> ExistAsync(Guid id);
        bool SaveAll();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> token);
        IEnumerable<T> Pagination(int page = 1, int count = 5);
        Task<int> Count();
        

    }
}
