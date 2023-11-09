using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Infrastructure.IRepository;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data;

namespace System.Infrastructure.Repository
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class 
    {
        private readonly AplicationDataContext _context;

        public GenericRepository(AplicationDataContext context)
        {
            _context = context;
        }

        public async Task<List<T>> Get()
        {
            return await _context.Set<T>().ToListAsync();
        }
       
        public IQueryable<T> GetAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }
        
        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        
        public async Task<T> Create(T entity)
        {
             _context.Set<T>().Add(entity);
             SaveAll();
            return entity;
        }

        public async Task<T> CreateRange(IEnumerable<T> entity)
        {
            _context.Set<T>().AddRange(entity);
             SaveAll();
            return (T)entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //this._context.Set<T>().Update(entity);
            SaveAll();
            return entity;
        }

        public async Task<List<T>> UpdateRange(List<T> entity)
        {
            //_context.Entry(entity).Status = EntityState.Modified;
            _context.Set<T>().UpdateRange(entity);
             SaveAll();
            return entity;
        }
        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
             SaveAll();
        }
        public async Task DeleteRange(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
             SaveAllAsync();
        }
        public void Delete(int id)
        {
            var entity = this._context.Set<T>().Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }
        
        public async Task<bool> SaveAllAsync()
        {
            return await this._context.SaveChangesAsync() > 0;
        }
        public  bool SaveAll()
        {
            return  this._context.SaveChanges() > 0;
        }

        public IQueryable<T> GetQueryable()
        {
            var myQueryable = _context.Set<T>().AsQueryable();
            return myQueryable;
        }

        public Task<bool> ExistAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> Pagination(int page = 1, int count = 5)
        {
            return _context.Set<T>().Skip((page - 1) * count).Take(count);
        }
        public async Task<int> Count()
        {
            return await _context.Set<T>().CountAsync();
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> token)
        {
            return _context.Set<T>().Where(token);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<C>> ExecuteQuerys<C>(C entity, string name, Dictionary<string, object> parameters) where C : class
        {
            var execString = "exec " + name;
            string query = "";
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    if(parameter.Value != null)
                    execString += " " + parameter.Key + " = " + parameter.Value + ",";
                    else
                        execString += " " + parameter.Key + " = NULL,";
            }
                query = execString.Substring(0, (execString.Length - 1)).ToString();
            
            
            return await _context.Set<C>().FromSqlRaw(query).AsNoTracking().ToListAsync();
        }



    }
}
