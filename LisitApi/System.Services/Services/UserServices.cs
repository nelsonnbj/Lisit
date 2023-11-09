using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Infrastructure.Configuration;
using System.Infrastructure.IRepository;
using System.Infrastructure.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly JwtConfig _jwtConfig;
        private readonly AplicationDataContext _dbcontent;

        public UserServices(IOptionsMonitor<JwtConfig> optionsMonitor,
                            AplicationDataContext dbcontent,
                            UserManager<Users> userManager,
                            RoleManager<Roles> roleManager,
                            SignInManager<Users> signInManager)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _dbcontent = dbcontent;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        #region Autentication

        public async Task<Users> GetUserAsync(string email)
        {

            return await _userManager.FindByEmailAsync(email);
        }
        public UserResponse ToUserResponse(Users user)
         {
             if (user == null)
             {
                 return null;
             }

             return new UserResponse
             {
                 Id = user.Id,
                 Email = user.Email,
                 PhoneNumber = user.PhoneNumber,
             };
         }
        public async Task<string> GeneratePasswordResetTokenAsync(Users user)
         {
             return await _userManager.GeneratePasswordResetTokenAsync(user);
         }
        public async Task<List<Roles>> Roles(bool isAdmin = false)
         {
            
            var response = await _roleManager.Roles.ToListAsync();
            if (!isAdmin)
            {
                var admin = await _roleManager.FindByNameAsync("Admin");
                response.Remove(admin);
                var aplicant = await _roleManager.FindByNameAsync("Aplicante");
                response.Remove(aplicant);
                var student = await _roleManager.FindByNameAsync("Estudiantes");
                response.Remove(student);
            }
            return response;
         }
        public async Task<IList<string>> GetRolesAsync(Users user)
         {

             var response = await _userManager.GetRolesAsync(user);
             return response;
         }
        public async Task<List<UserViewModel>> GetAlUsers()
         {
             try
             {
                 var users = await _userManager.Users.Select(x => new UserViewModel(x.Email, x.UserName)).ToListAsync();
                 return users;
             }
             catch (Exception)
             {
                 throw;
             }

         }
        public async Task<IdentityResult> AddUserAsync(Users user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(Users user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<IdentityResult> ResetPasswordAsync(Users user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(Users user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }
        public async Task<SignInResult> ValidatePasswordAsync(Users user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }
        public async Task<Users> GetUserAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
        public async Task<IdentityResult> ChangePasswordAsync(Users user, string oldPassword, string newPassword)
        {
            user.FirstLogin = false;
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
        public async Task<IdentityResult> UpdateUserAsync(Users user)
        {
            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> RemoveFromRoleAsync(Users user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result;
        }
        public async Task AddUserToRoleAsync(Users user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
        public async Task<IdentityResult> AddUserToRoleAsyncs(Users user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }
        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new Roles
                {
                    Name = roleName
                });
            }
        }
        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<IdentityResult> CreateRolAsync(string name)
        {
            return await _roleManager.CreateAsync(new Roles
            {
                Name = name
            });
        }
        public async Task<bool> IsUserInRoleAsync(Users user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
        public async Task<bool> CheckPasswordAsync(Users user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<Users> AddUserAsync(AddUserViewModel model, string path)
        {
            var Applicant = new Users
            {
                Email = model.Username,
                UserName = model.Username,
            };

            IdentityResult result = await _userManager.CreateAsync(Applicant, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            Users newUser = await GetUserAsync(model.Username);
            // await AddUserToRoleAsync(newUser, Applicant.UserType.ToString());
            return newUser;
        }

         #endregion

    }
}
