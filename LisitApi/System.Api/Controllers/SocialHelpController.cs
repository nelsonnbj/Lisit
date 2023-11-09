using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Infrastructure.DTO;
using System.Infrastructure.Services;
using System.Security.Claims;
using System.Threading.Tasks;

using SystemTheLastBugSpa.Data;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialHelpController : ControllerBase
    {

        private readonly ILogger<SocialHelpController> _logger;
        private readonly AplicationDataContext _dbcontent;
        private readonly SocialHelpServices _SocialHelpServices;

        public SocialHelpController(SocialHelpServices SocialHelpServices,AplicationDataContext dbcontent, ILogger<SocialHelpController> logger)
        {

            _logger = logger;
            _dbcontent = dbcontent;
            _SocialHelpServices = SocialHelpServices;
        }


        // GET: api/state
        /// <summary>
        /// Obtiene un resultado de todas las ayudas sociales
        /// </summary>     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _SocialHelpServices.GetSocialHelp();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        [HttpPost]
        //[Route("CrearSocialHelp")]
        //[Produces(typeof(AuthResult))]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegistrarSocialHelp([FromForm] SocialHelpDto SocialHelp)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;              
                var registrado = await _SocialHelpServices.AddSocialHelp(SocialHelp);               

                if (!registrado.IsSuccess)
                    return BadRequest(registrado.Message);

                _logger.LogInformation("Registro la ayuda social "+SocialHelp.Services +", usuario: "+ userEmail + ", Hora: "+DateTime.Now +"");
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Update: api/SocialHelp/UpdateSocialHelp
        /// <summary>
        /// Metodo utilizado para actualizar las ayudas sociales
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateSocialHelp([FromBody] SocialHelpDto model)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var result = await _SocialHelpServices.UpdateSocialHelp(model);
                if (result.IsSuccess)
                    return Ok(result);

                _logger.LogInformation("Se actualizo la ayuda social " + model.Services + ", usuario: " + userEmail + ", Hora: " + DateTime.Now + "");
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo  un error al cargar" + e.Message);
            }

        }
   
    }
}
