using System.Collections.Generic;

namespace System.Infrastructure.ViewModels
{
    public class PaginadorGenerico<T> where T : class
    {
        /// <summary>
        /// Página devuelta por la consulta actual.
        /// </summary>
        public int Actualpage { get; set; }
        /// <summary>
        /// Número de registros de la página devuelta.
        /// </summary>
        public int RecordsPerPage { get; set; }
        /// <summary>
        /// Total de registros de consulta.
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// Total de páginas de la consulta.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Texto de búsqueda de la consuta actual.
        /// </summary>
        public string CurrentSearch { get; set; }
        /// <summary>
        /// Columna por la que esta ordenada la consulta actual.
        /// </summary>
        //public string OrdenActual { get; set; }
        ///// <summary>
        ///// Tipo de ordenación de la consulta actual: ASC o DESC.
        ///// </summary>
        //public string TipoOrdenActual { get; set; }
        /// <summary>
        /// Resultado devuelto por la consulta a la tabla Customers
        /// en función de todos los parámetros anteriores.
        /// </summary>
        public IEnumerable<T> Result { get; set; }
    }
}
