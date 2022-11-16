using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using InternetBanking.Presentation.WebApp.MVC.Controllers;

namespace WebApp.InternetBanking.Presentation.WebApp.MVC.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        //private readonly ValidateUserSession _userSession;

        //public LoginAuthorize(ValidateUserSession userSession)
        //{
        //    _userSession = userSession;
        //}

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            //if (_userSession.HasUser())
            //{
            //    var controller = (UserController)context.Controller;
            //    context.Result = controller.RedirectToAction("index", "home");
            //}
            //else
            //{
            //    await next();
            //}
        }
    }
}
