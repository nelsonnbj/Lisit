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

namespace System.Infrastructure.Services
{
  public class PeopleServices
    {
        private readonly IPeopleRepository _people;
        private readonly IMapper _mapper;
        private readonly ILogger<PeopleServices> _logger;
        public PeopleServices(IConfiguration config, IPeopleRepository people, IMapper mapper, ILogger<PeopleServices> logger)
        {
            _people = people;
            _mapper = mapper;
            _logger = logger;
       
        }

        public async Task<List<People>> GetPeople()
        {
            try
            {
                var response = await _people.Get();
                var result = _mapper.Map<List<PeopleDto>>(response);
                return response;

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                return new List<People>();
            }

        }

        public async Task<People> GetByIdPeople(Guid id)
        {
            try
            {
                var response = await _people.GetById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> AddPeople(PeopleDto model)
        {

            try
            {
                var response = _mapper.Map<People>(model);
                await _people.Create(response);
                var result = _mapper.Map<PeopleDto>(response);
                return new Response { IsSuccess = true, Result = model };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<Response> UpdatePeople(PeopleDto model)
        {
            try
            {
                var response = _mapper.Map<People>(model);
                await _people.Update(response);
                var result = _mapper.Map<PeopleDto>(response);
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
