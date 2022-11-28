using InternetBanking.Controllers;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.InternetBanking.Middlewares
{
    public class AdminAuthorize : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public AdminAuthorize(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _httpContextAccesor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (user != null)
            {
                var isAdmin = user.Roles.Contains(Roles.Admin.ToString());

                if (!isAdmin)
                {
                    var controller = (HomeController)context.Controller;
                    context.Result = controller.RedirectToAction("AccessDenied", "User");
                }
                else await next();
            }
        }
    }
}
