using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Infrastructure.Configuration;
using System.Infrastructure.DTO;
using System.Infrastructure.Helpers;
using System.Infrastructure.IRepository;
using System.Infrastructure.Repository;
using System.Infrastructure.ViewModels;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Services
{
    public class AccountServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUserServices _userServices;
        private readonly UserManager<Users> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly RoleManager<Roles> _roleManager;
        private readonly RefreshTokensRepository _refreshTokensRepository;
        private readonly ISendMailServices _sendMailServices;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly ILogger<AccountServices> _logger;

        public AccountServices(IUserServices userServices, IOptionsMonitor<JwtConfig> optionsMonitor,
                               RoleManager<Roles> roleManager, UserManager<Users> userManager,
                               AplicationDataContext dbcontent, ISendMailServices sendMailServices,
                               TokenValidationParameters tokenValidationParams,
                               IConfiguration configuration, ILogger<AccountServices> logger)
        {
            _userServices = userServices;
            _jwtConfig = optionsMonitor.CurrentValue;
            _roleManager = roleManager;
            _userManager = userManager;
            _sendMailServices = sendMailServices;
            _refreshTokensRepository = new RefreshTokensRepository(dbcontent);
            _tokenValidationParams = tokenValidationParams;
            _configuration = configuration;
            _logger = logger;
        }
        private async Task<List<Claim>> GetAllValidClaims(Users user)
        {
            string document = "n/a";
            string applicantId = string.Empty;
            var _options = new IdentityOptions();
            //var _applicant = _applicantRepository.FindAll(a => a.Email == user.Email).FirstOrDefault();

            //if (_applicant != null)
            //{
            //    document = _applicant.Identification;
            //    applicantId = _applicant.Id.ToString();
            //}
            var rolName = await _roleManager.FindByIdAsync(user.RolesId.ToString());
            var claims = new List<Claim>
                {
                       new Claim(JwtRegisteredClaimNames.Email, user.Email),
                       new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
                       new Claim("id",user.Id.ToString()),
                       new Claim("ida",applicantId),
                       new Claim("ps",document),
                       new Claim(ClaimTypes.Role,rolName.Name),
                       new Claim("ft",user.FirstLogin.ToString()),
                       new Claim("cf",user.CompletedForm.ToString()),
                       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            //Getting the claims that we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //Get the user role and add it to the claims
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }
        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
        }
        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTimeVal;
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRefreshDTO tokenRefreshDTO)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                //Validation 1 - Validate JWT token format

                var tokenInvVerification = jwtTokenHandler.ValidateToken(tokenRefreshDTO.Token, _tokenValidationParams, out var validatedToken);

                //Validation 2 - Validate encryption alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                //Validation 3 - Validate expiry date
                var utcExpiryDate = long.Parse(tokenInvVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate >= DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                            {
                                "El token aún no ha caducado"
                            }
                    };
                }

                //Validation 4 - Validate existence of the token
                var storedToken = _refreshTokensRepository.GetAll().FirstOrDefault(x => x.Token == tokenRefreshDTO.Token);

                if (storedToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                            {
                                "la ficha no existe"
                            }
                    };
                }

                //Validation 5 - Validate if is used or not
                if (storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                            {
                                "Se ha utilizado el token"
                            }
                    };
                }

                //Validation 6 - Validate if is revoked
                if (storedToken.IsRevorked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                            {
                                "El token ha sido revocado"
                            }
                    };
                }

                //Validation 7 - Validate the id
                var jti = tokenInvVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                            {
                                "El token no coincide"
                            }
                    };
                }

                //update current token
                storedToken.IsUsed = true;
                await _refreshTokensRepository.Update(storedToken);
                await _refreshTokensRepository.SaveAllAsync();
                var res = storedToken.UserId.ToString();
                //Generate a new token
                var dbUser = await _userManager.FindByIdAsync(res);
                return await GenerateJwtToken(dbUser);

            }
            catch (Exception)
            {
                throw;
            }
        }
          
        public async Task<Result> ChangePassword(ChangePasswordDTO request)
        {
            try
            {
                var user = await _userServices.GetUserAsync(request.Email);
                if (user == null)
                {
                    return new Result()
                    {
                        Errors = new List<string>()
                         {
                            "usuario no existe",
                         },

                        Success = false
                    };
                }
                user.FirstLogin = false;
                IdentityResult userOperation = await _userServices.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

                if (userOperation == null)
                {
                    var result = new Result()
                    {
                        Errors = new List<string>()
                        {
                            "usuario no existe",
                        },
                        Message = userOperation.Errors.FirstOrDefault().Description,
                        Success = false
                    };
                    userOperation.Errors.ToList().ForEach(e => result.Errors.Add(e.Description));
                    return result;
                }
                return new Result()
                {
                    Success = true,
                    Message = "Contraseña cambiada con éxito"
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<AuthResult> GetNewToken(TokenRefreshDTO tokenRefreshDTO)
        {
            try
            {
                var result = await VerifyAndGenerateToken(tokenRefreshDTO);
                if (result != null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "Invalido tokens" },
                        Success = false
                    };
                }
                return new AuthResult
                {
                    Success = true,
                    RefreshToken = result.RefreshToken,
                    Token = result.Token

                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<AuthResult> GenerateJwtToken(Users user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevorked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(5),
                Token = RandomString(35) + Guid.NewGuid()
            };
            //await _refreshTokensRepository.Create(refreshToken);
            //await _refreshTokensRepository.SaveAllAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }
       
        public async Task<UserResult> Register(UserRegisterDTO user)
        {
            try
            {
                var existingUser = await _userServices.GetUserAsync(user.Email);

                if (existingUser != null)
                {
                    return new UserResult()
                    {
                        Errors = new List<string>() { "El correo enviado ya esta en uso.", },
                        Message = "El correo enviado ya esta en uso.",
                        Success = false
                    };
                }

                if (!await _roleManager.RoleExistsAsync(user.Rol))
                {
                    return new UserResult()
                    {
                        Errors = new List<string>() { "El rol no existe", },
                        Message = "El rol no existe",
                        Success = false
                    };
                }

                Roles role = await _roleManager.FindByNameAsync(user.Rol);

                var applicant = new Users()
                {
                    Email = user.Email,
                    UserName = user.Name,
                    RolesId = role.Id,
                    EmailConfirmed = true,
                    CompletedForm = true

                };

                var isCreated = await _userServices.AddUserAsync(applicant, user.Password);

                if (!isCreated.Succeeded)
                {
                    return new UserResult()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    };
                }

                return new UserResult { Applicant = applicant, Success = true };
            }
            catch
            {
                throw;
            }
        }

        public async Task<AuthResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var existingUser = await _userServices.GetUserAsync(loginDTO.Email);

                if (existingUser == null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Correo o contraseña es incorrecto",
                        },
                        Message = "Correo o contraseña es incorrecto",
                        Success = false
                    };
                }
                if (!existingUser.EmailConfirmed)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>()
                         {
                            "El correo electrónico no ha sido confirmado.",
                         },
                        Message = "El correo electrónico no ha sido confirmado.",
                        Success = false
                    };
                }
                if (existingUser.LockoutEnabled == true && existingUser.LockoutEnd != null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>()
                         {
                            "Usuario bloqueada o la usuario no existe.",
                         },
                        Message = "Usuario bloqueada o la usuario no existe.",
                        Success = false
                    };

                }

                var isCorrect = await _userServices.CheckPasswordAsync(existingUser, loginDTO.Password);

                if (!isCorrect)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>()
                         {
                            "Correo electrónico o la contraseña son incorrectos",
                         },
                        Message = "Correo electrónico o la contraseña son incorrectos",
                        Success = false
                    };
                }

                var jwtToken = await GenerateJwtToken(existingUser);

                return jwtToken;
            }
            catch (Exception)
            {
                //Se implementa el log
                throw;
            }
        } 
      
    }
}
