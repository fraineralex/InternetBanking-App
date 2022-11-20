using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Presentation.WebApp.MVC.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetBanking.Presentation.WebApp.MVC.Middlewares
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
            var currentlyUser = _httpContextAccesor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (currentlyUser != null)
            {
                var isAdmin = currentlyUser.Roles.Contains(Roles.Admin.ToString());
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
