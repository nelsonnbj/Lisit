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
    public class RegionController : ControllerBase
    {

        private readonly ILogger<RegionController> _logger;
        private readonly AplicationDataContext _dbcontent;
        private readonly RegionServices _regionServices;

        public RegionController(RegionServices regionServices,AplicationDataContext dbcontent, ILogger<RegionController> logger)
        {

            _logger = logger;
            _dbcontent = dbcontent;
            _regionServices = regionServices;
        }


        // GET: api/state
        /// <summary>
        /// Obtiene un resultado de todos las regiones
        /// </summary>     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _regionServices.GetRegiones();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        [HttpPost]
        //[Route("CrearRegion")]
        //[Produces(typeof(AuthResult))]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegistrarRegion([FromForm] RegionDto region)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var registrado = await _regionServices.AddRegion(region);

                if (!registrado.IsSuccess)
                    return BadRequest(registrado.Message);

                _logger.LogInformation("Agrego la region " + region.Nombre + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Update: api/Region/UpdateRegion
        /// <summary>
        /// Metodo utilizado para actualizar las regiones
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateRegion([FromBody] RegionDto model)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var result = await _regionServices.UpdateRegion(model);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Atualizo la region " + model.Nombre + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                    return Ok(result);
                }
                

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo  un error al cargar" + e.Message);
            }

        }

        // Update: api/Region/5
        /// <summary>
        /// Metodo utilizado para eliminar las regiones
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
                var response = await _regionServices.DeleteRegion(id);
                _logger.LogInformation("Elimino la region " + response.Result + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar");
            }


        }
    }
}
