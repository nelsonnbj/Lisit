namespace System.WebApi.DTos.Pagination
{
    public class PaginacionDTo
    {
        public int Pagina { get; set; } = 1;

        private int cantidadRegistrosPorPagina = 20;

        private int CantidadMaxRegistroPorPagina = 50;

        public int CantidadRegistrosPorPagina
        {
            get => cantidadRegistrosPorPagina;

            set
            {
                cantidadRegistrosPorPagina = (value > CantidadMaxRegistroPorPagina) ? CantidadMaxRegistroPorPagina : value;
            }
        }

        public string Name { get; set; }

        public string Abr { get; set; }

        public Guid? StatusId { get; set; }

    }
}
