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
  public class RegionServices
    {
        private readonly IRegionesRepository _region;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionServices> _logger;
        public RegionServices(IConfiguration config, IRegionesRepository region, IMapper mapper, ILogger<RegionServices> logger)
        {
            _region = region;
            _mapper = mapper;
            _logger = logger;
       
        }

        public async Task<IQueryable<Region>> GetRegiones()
        {
            try
            {
                var response = await _region.GetRegion();            
                return response;

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                return null;
            }

        }

        public async Task<Region> GetByIdRegion(Guid id)
        {
            try
            {
                var response = await _region.GetById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> AddRegion(RegionDto model)
        {

            try
            {
                var response = _mapper.Map<Region>(model);
                await _region.Create(response);
                var result = _mapper.Map<RegionDto>(response);
                return new Response { IsSuccess = true, Result = model };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<Response> UpdateRegion(RegionDto model)
        {
            try
            {
                var response = _mapper.Map<Region>(model);
                await _region.Update(response);
                var result = _mapper.Map<RegionDto>(response);
                return new Response { IsSuccess = true, Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = e.Message };
            }

        }

        public async Task<Response> DeleteRegion(Guid id)
        {
            try
            {
                var response = await _region.GetById(id);
                if (response == null)
                {
                    return new Response { IsSuccess = false, Message = "No se encontro el id" };
                }
                await _region.Update(response);
                return new Response { IsSuccess = true, Result = response.Nombre };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = "Bad request" };
            }
        }
    }
}
