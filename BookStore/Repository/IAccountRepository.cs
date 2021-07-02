using BookStore.Areas.Admin.Models;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignupUserModel userModel);
        Task<SignInResult> PasswordLoginAsync(LoginUserModel loginUserModel);
        Task LogoutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
        List<ApplicationUser> GetAllApplicationUsers();
        List<RolesModel> GetAllRoles();
    }
}