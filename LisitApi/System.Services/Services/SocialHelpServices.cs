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
  public class SocialHelpServices
    {
        private readonly ISocialHelpRepository _socialHelp;
        private readonly IMapper _mapper;
        private readonly ILogger<SocialHelpServices> _logger;
        public SocialHelpServices(IConfiguration config, ISocialHelpRepository socialHelp, IMapper mapper, ILogger<SocialHelpServices> logger)
        {
            _socialHelp = socialHelp;
            _mapper = mapper;
            _logger = logger;
       
        }

        public async Task<List<SocialHelp>> GetSocialHelp()
        {
            try
            {
                var response = await _socialHelp.Get();
                var result = _mapper.Map<List<SocialHelpDto>>(response);
                return response;

            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                return new List<SocialHelp>();
            }

        }

        public async Task<SocialHelp> GetByIdSocialHelp(Guid id)
        {
            try
            {
                var response = await _socialHelp.GetById(id);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al cargar  {0}, {@e}", e.Message, e);
                throw;
            }

        }

        public async Task<Response> AddSocialHelp(SocialHelpDto model)
        {

            try
            {
                var response = _mapper.Map<SocialHelp>(model);
                await _socialHelp.Create(response);
                var result = _mapper.Map<SocialHelpDto>(response);
                return new Response { IsSuccess = true, Result = model };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }
        }

        public async Task<Response> UpdateSocialHelp(SocialHelpDto model)
        {
            try
            {
                var response = _mapper.Map<SocialHelp>(model);
                await _socialHelp.Update(response);
                var result = _mapper.Map<SocialHelpDto>(response);
                return new Response { IsSuccess = true, Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return new Response { IsSuccess = false };
            }

        }
    }
}
