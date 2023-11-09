using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Infrastructure.DTO;
using System.Infrastructure.Services;
using System.Security.Claims;
using System.Threading.Tasks;

using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesByPeopleController : ControllerBase
    {

        private readonly ILogger<ServicesByPeopleController> _logger;
        private readonly AplicationDataContext _dbcontent;
        private readonly ServicesByPeopleServices _ServicesByPeopleServices;

        public ServicesByPeopleController(ServicesByPeopleServices ServicesByPeopleServices,AplicationDataContext dbcontent, ILogger<ServicesByPeopleController> logger)
        {

            _logger = logger;
            _dbcontent = dbcontent;
            _ServicesByPeopleServices = ServicesByPeopleServices;
        }


        // GET: api/state
        /// <summary>
        /// Obtiene un resultado de todas que poseen ayudas social
        /// </summary>     
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Get(int? comunaId, int? peopleId, int? socialHelpId)
        {
            try
            {
                var response = await _ServicesByPeopleServices.GetServicesByPeople(comunaId, peopleId, socialHelpId);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


        [HttpPost]
        //[Route("CrearServicesByPeople")]
        //[Produces(typeof(AuthResult))]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegistrarServicesByPeople([FromForm] ServicesByPeopleDto ServicesByPeople)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var registrado = await _ServicesByPeopleServices.AddServicesByPeople(ServicesByPeople);

                if (!registrado.IsSuccess)
                    return BadRequest(registrado.Message);

                _logger.LogInformation("Se le asigno la ayuda social a la siguiente persona "+ ServicesByPeople.PeopleId + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                return Ok(registrado.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Update: api/ServicesByPeople/UpdateServicesByPeople
        /// <summary>
        /// Metodo utilizado para actualizar las ayudas asignada a una persona
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateServicesByPeople([FromBody] ServicesByPeopleDto model)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var result = await _ServicesByPeopleServices.UpdateServicesByPeople(model);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Se le asigno la ayuda social a la siguiente persona " + model.PeopleId + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
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
