using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Helpers
{
    public class GenericPage<R>
    {
        public Result<R> Get(List<R> entities, int itemCount, int page, int paginationConfig)
        {
            try
            {
                int _totalPage = 0;

                if (itemCount > 0)
                    _totalPage = Convert.ToInt32(Math.Ceiling(((decimal)itemCount) / paginationConfig));
                else
                    _totalPage = 1;
                var result = entities.Skip((page - 1 ) * paginationConfig).Take(paginationConfig).ToList();
                return new Result<R> { Records = entities, Pagination = new PaginationResult { CountPage = _totalPage, CurrentPage = page } };
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
    public class Result<R>
    {
        public List<R> Records { get; set; }
        public PaginationResult Pagination { get; set; }
    }
    public class PaginationResult
    {
        public int CurrentPage { get; set; }
        public int CountPage { get; set; }
    }
}
