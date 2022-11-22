using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Admin.Auth;
using InternetBanking.Presentation.WebApp.MVC.Middlewares;
using InternetBanking.Presentation.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetBanking.Presentation.WebApp.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISavingsAccountsService _savingsAccountsService;
        private readonly ICreditCardsService _cardsService;
        private readonly ILoansService _loansService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HomeController(IUserService userService, RoleManager<IdentityRole> roleManager, ISavingsAccountsService savingsAccountsService, ICreditCardsService cardsService, ILoansService loansService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _roleManager = roleManager;
            _savingsAccountsService = savingsAccountsService;
            _loansService = loansService;
            _cardsService = cardsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {

            var currentlyUser = HttpContext.Session.Get<AuthenticationResponse>("user");
            var isAdmin = currentlyUser.Roles.Contains(Roles.Admin.ToString());

            if (isAdmin)
            {
                return RedirectToAction("HomeAdmin");
            }

            return RedirectToAction("HomeClient");
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        public IActionResult HomeAdmin()
        {
            return View();
        }

        [ServiceFilter(typeof(ClientAuthorize))]
        public async Task<IActionResult> HomeClient()
        {
            var currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            ViewBag.SavingAccounts = await _savingsAccountsService.GetAllSavingsAccountsViewModels(currentlyUser.Id);
            ViewBag.CreditCards = await _cardsService.GetAllCreditCardsViewModels(currentlyUser.Id);
            ViewBag.Loans = await _loansService.GetAllLoansViewModels(currentlyUser.Id);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}