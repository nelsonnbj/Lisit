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
    public class ComunaController : ControllerBase
    {

        private readonly ILogger<ComunaController> _logger;
        private readonly AplicationDataContext _dbcontent;
        private readonly ComunaServices _comunaServices;

        public ComunaController(ComunaServices comunaServices,AplicationDataContext dbcontent, ILogger<ComunaController> logger)
        {

            _logger = logger;
            _dbcontent = dbcontent;
            _comunaServices = comunaServices;
        }


        // GET: api/state
        /// <summary>
        /// Obtiene un resultado de todos las Comunas
        /// </summary>     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _comunaServices.GetComuna();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        [HttpPost]
        //[Route("CrearComuna")]
        //[Produces(typeof(AuthResult))]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegistrarComuna([FromForm] ComunaDto Comuna)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var registrado = await _comunaServices.AddComuna(Comuna);

                if (!registrado.IsSuccess)
                    return BadRequest(registrado.Message);

                _logger.LogInformation("Registro la comuna " + Comuna.Nombre + ", el usuario: " + userEmail + ", Hora: " + DateTime.Now + "");
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Update: api/Comuna/UpdateComuna
        /// <summary>
        /// Metodo utilizado para actualizar las comunas
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateComuna([FromBody] ComunaDto model)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var result = await _comunaServices.UpdateComuna(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                    _logger.LogInformation("Actualizo la comuna " + model.Nombre + ", el usuario: " + userEmail + ", Hora: " + DateTime.Now + "");
                }
                   

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo  un error al cargar" + e.Message);
            }

        }

        // Update: api/Comuna/5
        /// <summary>
        /// Metodo utilizado para eliminar las comunas
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpDelete("[action]/")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var response = await _comunaServices.DeleteComuna(id);
                _logger.LogInformation("Elimino la comuna " + response.Result + ", el usuario: " + userEmail + ", Hora: " + DateTime.Now + "");
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar");
            }


        }
    }
}
