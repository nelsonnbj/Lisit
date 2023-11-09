using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Infrastructure.Configuration;
using System.Infrastructure.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.IRepository
{
    public interface IUserServices
    {
        Task<Users> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(Users user, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(Users user);
        Task<Users> GetUserAsync(Guid userId);
        Task<IdentityResult> RemoveFromRoleAsync(Users user, string roleName);
        Task<IList<string>> GetRolesAsync(Users Users);
        Task<IdentityResult> AddUserToRoleAsyncs(Users user, string roleName);
        Task<List<UserViewModel>> GetAlUsers();
        Task<IdentityResult> CreateRolAsync(string name);
        Task<bool> RoleExistsAsync(string roleName);
        Task<List<Roles>> Roles(bool isAdmin);
        Task<string> GeneratePasswordResetTokenAsync(Users user);
        Task<IdentityResult> ResetPasswordAsync(Users user, string token, string password);
        Task<IdentityResult> ConfirmEmailAsync(Users user, string token);

        Task<IdentityResult> ChangePasswordAsync(Users user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(Users user);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(Users user, string roleName);

        Task<bool> IsUserInRoleAsync(Users user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task<SignInResult> ValidatePasswordAsync(Users user, string password);

        Task LogoutAsync();
        UserResponse ToUserResponse(Users user);
        Task<Users> AddUserAsync(AddUserViewModel model, string path);
        Task<bool> CheckPasswordAsync(Users user, string roleName);


    }
}
