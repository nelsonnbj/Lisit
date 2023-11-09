using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Infrastructure.DTO;
using System.Infrastructure.IRepository;
using System.Infrastructure.ViewModels;
using System.Linq;
using System.Threading.Tasks;

using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Services
{
    public class CountryServices
    {
        private readonly ICountryRepository _country;
        private readonly ILogger<CountryServices> _logger;
        private readonly IMapper _mapper;
        public CountryServices(ICountryRepository country, IMapper mapper, ILogger<CountryServices> logger)
        {
            _country = country;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IQueryable<Country>> GetCountry()
        {
            try
            {
                var response = await _country.GetCaontry();
               //var result = _mapper.Map<List<CountryDtos>>(response);
                return response;

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                return null;
            }

        }

        public async Task<Country> GetByIdCountry(Guid id)
        {
            try
            {
                var response = await _country.GetById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> AddCountry(CountryDtos model)
        {

            try
            {
                var response = _mapper.Map<Country>(model);                
                await _country.Create(response);
                var result = _mapper.Map<CountryDtos>(response);
                return new Response { IsSuccess = true, Result = model };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<Response> UpdateCountry(CountryDtos model)
        {
            try
            {
                var response = _mapper.Map<Country>(model);            
                await _country.Update(response);
                var result = _mapper.Map<CountryDtos>(response);
                return new Response { IsSuccess = true, Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = e.Message };
            }

        }

        public async Task<Response> DeleteCountry(Guid id)
        {
            try
            {
                var response = await _country.GetById(id);
                if (response == null)
                {
                    return new Response { IsSuccess = false, Message = "No se encontro el id" };
                }               
                await _country.Update(response);
                return new Response { IsSuccess = true, Result = response.Description };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = "Bad request" };
            }
        }
    }
}
