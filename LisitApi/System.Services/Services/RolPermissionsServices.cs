using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Infrastructure.DTO;
using System.Infrastructure.IRepository;
using System.Infrastructure.Repository;
using System.Infrastructure.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Services
{
    public class RolPermissionsServices
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RolPermissionsServices> _logger;
        private readonly IRolPermissionsRepository _rolPermissionsRepository;
        private int _pagination;

        public RolPermissionsServices(IConfiguration config, IRolPermissionsRepository rolPermissionsRepository,
                                  IMapper mapper, ILogger<RolPermissionsServices> logger)
        {
            _rolPermissionsRepository = rolPermissionsRepository;
            _mapper = mapper;
            _logger = logger;
            _pagination = Convert.ToInt32(config["Paginator"]);
        }
        public async Task<List<RolPermissionsDTO>> Get()
        {
            try
            {
                var response = await _rolPermissionsRepository.Get();
                var result = _mapper.Map<List<RolPermissionsDTO>>(response);
                return result;

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                return new List<RolPermissionsDTO>();
            }
        }
        public async Task<List<RolPermissions>> GetByIdRol(Guid rolId)
        {
            try
            {
                var results =  _rolPermissionsRepository.FindAll(rp => rp.RolId == rolId).ToList();
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }
        public async Task<RolPermissionsDTO> GetById(Guid id)
        {
            try
            {
                var result = await _rolPermissionsRepository.GetById(id);
                return _mapper.Map<RolPermissions, RolPermissionsDTO>(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> Add(List<RolPermissionsDTO> models)
        {

            try
            {
                foreach (var model in models)
                {
                    var response = _mapper.Map<RolPermissions>(model);
                    // verificar si existe.
                    var entity = _rolPermissionsRepository.FindAll(i => i.PermissionsId == response.PermissionsId && i.RolId == response.RolId).FirstOrDefault();
                    if (entity == null)
                    {
                        await _rolPermissionsRepository.Create(response);
                    }
                }
                return new Response { IsSuccess = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }
        }

        public async Task<Response> Update(List<RolPermissionsDTO> models)
        {
            try
            {
                var idRol = models[0].RolId;
                var rolpermisions =  _rolPermissionsRepository.FindAll(rp => rp.RolId == idRol).ToList();
                _rolPermissionsRepository.DeleteRange(rolpermisions);
                foreach (var model in models)
                {
                    var response = _mapper.Map<RolPermissions>(model);
                    // verificar si existe.
                    var entity = _rolPermissionsRepository.FindAll(i => i.PermissionsId == response.PermissionsId && i.RolId == response.RolId).FirstOrDefault();
                    if (entity == null)
                    {
                        await _rolPermissionsRepository.Create(response);
                    }
                }
                return new Response { IsSuccess = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }
        }

        public async Task<Response> Delete(Guid id)
        {
            try
            {
                var response = await _rolPermissionsRepository.GetById(id);
                if (response == null)
                {
                    return new Response { IsSuccess = false, Message = "No se encontro el id" };
                }
                response.Active = false;
                await _rolPermissionsRepository.Update(response);
                return new Response { IsSuccess = true, Result = response };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false, Message = "Bad request" };
            }
        }
    }
}
