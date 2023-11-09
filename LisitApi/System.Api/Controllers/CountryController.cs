using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Infrastructure.Configuration;
using System.Infrastructure.DTO;
using System.Infrastructure.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly ILogger<CountryController> _logger;
        private readonly AplicationDataContext _dbcontent;
        private readonly CountryServices _paisServices;

        public CountryController(CountryServices paisServices,AplicationDataContext dbcontent, ILogger<CountryController> logger)
        {

            _logger = logger;
            _dbcontent = dbcontent;
            _paisServices = paisServices;
        }


        // GET: api/state
        /// <summary>
        /// Obtiene un resultado de todos los paises
        /// </summary>     
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
              var response = await _paisServices.GetCountry();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar los datos");
            }
        }


   
        //[Route("CrearPais")]
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegistrarCountry([FromForm] CountryDtos pais)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var registrado = await _paisServices.AddCountry(pais);

                if (!registrado.IsSuccess)
                    return BadRequest(registrado.Message);

                _logger.LogInformation("Registro el pais " + pais.Description + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                return Ok("Registrado Correctamente");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Update: api/Contry/UpdateCountry
        /// <summary>
        /// Metodo utilizado para actualizar los paises
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPut("[action]")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryDtos model)
        {
            try
            {
                var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var result = await _paisServices.UpdateCountry(model);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Actualizo el pais " + model.Description + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                    return Ok(result);
                }
                   

                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo  un error al cargar" + e.Message);
            }

        }

        // Update: api/Contry/5
        /// <summary>
        /// Metodo utilizado para eliminar los paises
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
                var response = await _paisServices.DeleteCountry(id);
                _logger.LogInformation("Elimino el pais " + response.Result + ", el usuario: " + userEmail + ", fecha: " + DateTime.Now + "");
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Hubo  un error al cargar");
            }


        }
    }
}
