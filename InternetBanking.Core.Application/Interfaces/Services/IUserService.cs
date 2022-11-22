using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IUserService //: IGenericService<SaveUserViewModel, SaveUserViewModel, ApplicationUser>
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task SignOutAsync();
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
    }
}
