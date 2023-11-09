using System.Collections.Generic;
using System.Data;
using System.Infrastructure.DTO;
using System.Infrastructure.IRepository;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;
using SystemTheLastBugSpa.Data.Procedure;

namespace System.Infrastructure.Repository
{
    public class ServicesByPeopleRepository : GenericRepository<ServicesByPeople>, IServicesByPeopleRepository
    {
        private readonly AplicationDataContext context;
        public ServicesByPeopleRepository(AplicationDataContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Metodo para obtener los datos de los servicios asignado a una persona
        /// </summary>
        /// <param name="comunaId">Primary key de la comuna</param>
        /// <returns></returns>
        public async Task<IEnumerable<ServicesByPeopleDescription>> GetServicesByPeopleDescription(int? comunaId, int? peopleId, int? socialHelpId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@comunaId", comunaId);
            parameters.Add("@PeopleId", peopleId);
            parameters.Add("@SocialHelpId", socialHelpId);
            return await ExecuteQuerys(new ServicesByPeopleDescription(), "PeopleByServicesDescription", parameters);

        }
    }
}
