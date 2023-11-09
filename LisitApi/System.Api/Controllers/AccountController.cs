using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Infrastructure.Configuration;
using System.Infrastructure.DTO;
using System.Infrastructure.Services;
using System.Infrastructure.ViewModels;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;

namespace System.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        //private readonly TokenValidationParameters _tokenValidationParams;
        private readonly AplicationDataContext _dbcontent;
        private readonly AccountServices _accountServices;

        public AccountController(AccountServices accountServices, AplicationDataContext dbcontent, ILogger<AccountController> logger)
        {

            _logger = logger;
            _dbcontent = dbcontent;
            _accountServices = accountServices;
        }



        /// <summary>
        /// Metodo utilizado para logiar a los usuarios
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="user"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpPost]
        [Route("Login")]
        [Produces(typeof(AuthResult))]
        public async Task<IActionResult> InicioSeccion([FromBody] LoginDTO user)
        {
            try
            {
                var result = await _accountServices.Login(user);

                if (result.Success)
                    return Ok(result);

                _logger.LogInformation("El siguiente usuario " + user.Email + "inicio sección");
                return BadRequest(result);

            }
            catch (Exception)
            {
                _logger.LogError("El siguiente usuario " + user.Email + "no pudo logearse");
                return BadRequest();
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Register")]
        [Produces(typeof(AuthResult))]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                var register = await _accountServices.Register(user);

                if (!register.Success)
                    return BadRequest(register);

                _logger.LogInformation("El siguiente usuario " + user.Email + "se registro");
                return Ok("Usuario Registrador Correctamente");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    

        /// <summary>
        /// Metodo utilizado para cambiar los claves a los usuarios sin roles
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto creado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ChangePassword")]
        [Produces(typeof(Result))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response
                    {
                        IsSuccess = false,
                        Message = "Bad request",
                        Result = ModelState
                    });
                }
                var result = await _accountServices.ChangePassword(request);
                if (!result.Success)
                    return BadRequest(result);

                _logger.LogInformation("El siguiente usuario "+ request.Email + "Cambio su contraseña");
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }          
    }
}
