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
  public class ComunaServices
    {
        private readonly IComunaRepository _comuna;
        private readonly IMapper _mapper;
        private readonly ILogger<ComunaServices> _logger;
        public ComunaServices(IConfiguration config, IComunaRepository Comuna, IMapper mapper, ILogger<ComunaServices> logger)
        {
            _comuna = Comuna;
            _mapper = mapper;
            _logger = logger;
       
        }

        public async Task<List<Comuna>> GetComuna()
        {
            try
            {
                var response = await _comuna.Get();
                var result = _mapper.Map<List<ComunaDto>>(response);
                return response;

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                return new List<Comuna>();
            }

        }

        public async Task<Comuna> GetByIdComuna(Guid id)
        {
            try
            {
                var response = await _comuna.GetById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> AddComuna(ComunaDto model)
        {

            try
            {
                var response = _mapper.Map<Comuna>(model);
                await _comuna.Create(response);
                var result = _mapper.Map<ComunaDto>(response);
                return new Response { IsSuccess = true, Result = model };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }
        }

        public async Task<Response> UpdateComuna(ComunaDto model)
        {
            try
            {
                var response = _mapper.Map<Comuna>(model);
                await _comuna.Update(response);
                var result = _mapper.Map<ComunaDto>(response);
                return new Response { IsSuccess = true, Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }

        }

        public async Task<Response> DeleteComuna(Guid id)
        {
            try
            {
                var response = await _comuna.GetById(id);
                if (response == null)
                {
                    return new Response { IsSuccess = false, Message = "No se encontro el id" };
                }
                await _comuna.Update(response);
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
