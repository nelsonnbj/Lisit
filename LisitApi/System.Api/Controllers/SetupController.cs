using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Infrastructure.IRepository;
using System.Threading.Tasks;

namespace System.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
   
    public class SetupController : ControllerBase
    {
        private readonly ILogger<SetupController> _logger;
        private readonly IUserServices _userServices;

        public SetupController(IUserServices userServices, ILogger<SetupController> logger)
        {
            _userServices = userServices;
            _logger = logger;
        }
        
        // Get: api/Setup/
        /// <summary>
        /// Obtiene  la lista de los Roles.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles(bool isAdmin = false)
        {
            try
            {
                var roles = await _userServices.Roles(isAdmin);
                return Ok(roles);
            }
            catch (Exception e)
            {

                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return BadRequest("Hubo un error al guardar");
            }


        }
        
        // Get: api/Setup/5
        /// <summary>
        /// Obtiene  la lista de los Usuarios.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAlUsers()
        {
            try
            {
                var users = await _userServices.GetAlUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return BadRequest("Hubo un error al guardar");
            }

        }
        // Get: api/Setup/5
        /// <summary>
        /// Obtiene  el rol de usuario atravez del email.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="email"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            try
            {
                var user = await _userServices.GetUserAsync(email);

                if (user == null) // User does not exist
                {
                    _logger.LogInformation($"The User {email} does not exist");
                    return BadRequest(new { error = "User does not exist" });
                }
                //return the roles
                var roles = await _userServices.GetRolesAsync(user);

                return Ok(roles);
            }
            catch (Exception e)
            {

                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return BadRequest("Hubo un error al guardar");
            }

        }
       
        // Post: api/Setup/5
        /// <summary>
        /// Metodo encargado de crear Roles
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="name"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string name)
        {

            // check f the role exist
            var roleExist = await _userServices.RoleExistsAsync(name);

            if (!roleExist)//checks on the role exist status
            {
                var roleResult = await _userServices.CreateRolAsync(name);

                //we need to check if the role has been added successfully
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The Role {name} has been added successfully");
                    return Ok(new
                    {
                        result = $"The role {name} has been added sucessfully"
                    });
                }
                else
                {
                    _logger.LogInformation($"The Role {name} has not been added successfully");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not beend sucessfully"
                    });
                }
            }

            return BadRequest(new { error = "Role already exist" });

        }
        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            //check if the user exist
            var user = await _userServices.GetUserAsync(email);

            if (user == null) // User does not exist
            {
                _logger.LogInformation($"The User {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }

            //check if the role exist
            var roleResult = await _userServices.CreateRolAsync(email);

            if (roleResult.Succeeded)
            {
                _logger.LogInformation($"The Role {email} does not exist");
                return BadRequest(new { error = "Role does not exist" });
            }

            var result = await _userServices.AddUserToRoleAsyncs(user, roleName);

            if (result.Succeeded)
            {
                //check if the user is assigned to the role susccessfully
                return Ok(new { result = "Success, user has been added to the role" });
            }
            else
            {
                _logger.LogInformation($"The User was not able to added to the role ");
                return BadRequest(new { error = "The User was not able to added to the role" });
            }
        }

        // Get: api/Setup/5
        /// <summary>
        /// Metodo que remueve el rol a los usuarios
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="roleName"></param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpPost]
        [Route("RevmoeUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {

            try
            {
                var user = await _userServices.GetUserAsync(email);

                if (user == null) // User does not exist
                {
                    _logger.LogInformation($"The User {email} does not exist");
                    return BadRequest(new { error = "User does not exist" });
                }

                //Role Exist
                var roleResult = await _userServices.CreateRolAsync(email);

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The Role {email} does not exist");
                    return BadRequest(new { error = "Role does not exist" });
                }

                var result = await _userServices.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return Ok(new { result = $"User {email} has been removed from role {roleName}" });
                }
                return BadRequest(new { error = $"Unable to  remove User {email} from role {roleName}" });
            }
            catch (Exception e)
            {

                _logger.LogError("Hubo  un error al guardad {0}, {@e}", e.Message, e);
                return BadRequest("Hubo un error al guardar");
            }

        }
        
    }

}
