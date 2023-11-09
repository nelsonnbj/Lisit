
using System.Collections.Generic;
using System.Infrastructure.DTO;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;
using SystemTheLastBugSpa.Data.Procedure;

namespace System.Infrastructure.IRepository
{
    public interface IServicesByPeopleRepository : IGenericRepository<ServicesByPeople>
    {
        /// <summary>
        /// Metodo para obtener los datos de los servicios asignado a una persona
        /// </summary>
        /// <param name="comunaId">Primary key de la comuna</param>
        /// <returns></returns>
       Task<IEnumerable<ServicesByPeopleDescription>> GetServicesByPeopleDescription(int? comunaId, int? peopleId, int? socialHelpId);
    }
}
