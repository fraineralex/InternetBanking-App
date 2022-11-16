using Microsoft.AspNetCore.Http;
using InternetBanking.Core.Application.Dtos.Account;
//using InternetBanking.Core.Application.Helpers;
//using InternetBanking.Core.Application.ViewModels.User;

namespace WebApp.InternetBanking.Presentation.WebApp.MVC.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            //AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            //if (userViewModel == null)
            //{
            //    return false;
            //}
            return true;
        }
    }
}
