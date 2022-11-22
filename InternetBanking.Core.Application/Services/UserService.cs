using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;

namespace InternetBanking.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterUserAsync(registerRequest, origin);

        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRquest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRquest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);

        }
    }
}
