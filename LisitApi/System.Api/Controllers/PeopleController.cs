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
    public class PeopleController : ControllerBase
    {

        private readonly ILogger<PeopleController> _logger;
        private readonly AplicationDataContext _dbcontent;
        private readonly PeopleServices _peopleServices;

        public PeopleController(PeopleServices peopleServices,AplicationDataContext dbcontent, ILogger<PeopleController> logger)
        {

            _logger = logger;
            _dbcontent = dbcontent;
            _peopleServices = peopleServices;
        }


        // GET: api/state
        /// <summary>
        /// Obtiene un resultado de todos las personas
        /// </summary>     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _peopleServices.GetPeople();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        [HttpPost]
        //[Route("CrearPeople")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegistrarPeople([FromForm] PeopleDto People)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var registrado = await _peopleServices.AddPeople(People);

                if (!registrado.IsSuccess)
                    return BadRequest(registrado.Message);

                _logger.LogInformation("Agrego a la persona " + People.Name + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                return Ok("Persona Registrada Correctamente");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Update: api/People/UpdatePeople
        /// <summary>
        /// Metodo utilizado para actualizar las personas
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdatePeople([FromBody] PeopleDto model)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var result = await _peopleServices.UpdatePeople(model);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Actualizo la persona " + model.Name + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                    return Ok(result);
                }
              

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo  un error al cargar" + e.Message);
            }

        }
   
    }
}
