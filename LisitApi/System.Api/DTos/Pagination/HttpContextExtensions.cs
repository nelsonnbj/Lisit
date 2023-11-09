using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebApi.DTos.Pagination
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParamentrosPaginacion<T>(this HttpContext httpContext, IQueryable<T> queryable, int cantidadRegistroPorPagina)
        {
            double cantidad = await queryable.CountAsync();
            double CantidadPaginas = Math.Ceiling(cantidad / cantidadRegistroPorPagina);
            httpContext.Response.Headers.Add("cantidadPaginas", CantidadPaginas.ToString());

        }
    }
}
