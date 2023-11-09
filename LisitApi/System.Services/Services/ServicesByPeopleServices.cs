using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Infrastructure.DTO;
using System.Infrastructure.IRepository;
using System.Infrastructure.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;
using SystemTheLastBugSpa.Data.Procedure;

namespace System.Infrastructure.Services
{
  public class ServicesByPeopleServices
    {
        private readonly IServicesByPeopleRepository _servicesByPeople;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicesByPeopleServices> _logger;
        public ServicesByPeopleServices(IConfiguration config, IServicesByPeopleRepository servicesByPeople, IMapper mapper, ILogger<ServicesByPeopleServices> logger)
        {
            _servicesByPeople = servicesByPeople;
            _mapper = mapper;
            _logger = logger;
       
        }

        public async Task<IEnumerable<ServicesByPeopleDescription>> GetServicesByPeople(int? comunaId, int? peopleId, int? socialHelpId)
        {
            try
            {
                var response = await _servicesByPeople.GetServicesByPeopleDescription(comunaId,peopleId,socialHelpId);                
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                return new List<ServicesByPeopleDescription>();
            }

        }

        public async Task<ServicesByPeople> GetByIdServicesByPeople(Guid id)
        {
            try
            {
                var response = await _servicesByPeople.GetById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> AddServicesByPeople(ServicesByPeopleDto model)
        {

            try
            {
                var response = _mapper.Map<ServicesByPeople>(model);
                var servicesAdd = _servicesByPeople.Get().Result
                                   .Where(x=>x.PeopleId == model.PeopleId && x.SocialHelpId == model.SocialHelpId)
                                   .OrderByDescending(s=>s.CreateDate)
                                   .FirstOrDefault();
              if(servicesAdd != null)
                {
                    if(servicesAdd.CreateDate.Year == DateTime.Now.Year)
                        return new Response { IsSuccess = false, Result = model, Message ="Esta persona ya posee este servicio" };
                }
                response.CreateDate = DateTime.Now;
                await _servicesByPeople.Create(response);
                var result = _mapper.Map<ServicesByPeopleDto>(response);
                return new Response { IsSuccess = true, Result = model, Message = "Servicio agregado" };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<Response> UpdateServicesByPeople(ServicesByPeopleDto model)
        {
            try
            {
                var response = _mapper.Map<ServicesByPeople>(model);
                await _servicesByPeople.Update(response);
                var result = _mapper.Map<ServicesByPeopleDto>(response);
                return new Response { IsSuccess = true, Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = e.Message };
            }

        }
    }
}
