using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.InternetBanking.Middlewares;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles = "Client")]

    public class TransferBetweenAccountController : Controller
    {
        private readonly IPaymentService _paymentSvc;
        private readonly IBeneficiaryService _recipientService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse currentlyUser;

        public TransferBetweenAccountController(IHttpContextAccessor httPContextAccesor, IPaymentService paymentService, IProductService productService, IUserService userService, IBeneficiaryService recipientService)
        {
            _httpContextAccessor = httPContextAccesor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _paymentSvc = paymentService;
            _productService = productService;
            _userService = userService;
            _recipientService = recipientService;
        }
        public async Task<IActionResult> TransferBetweenAccounts()
        {
            if(currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "HomeAdmin" });
            }

            ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> TransferBetweenAccounts(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "HomeAdmin" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                return View(vm);
            }

            SavePaymentViewModel transferViewModel = new SavePaymentViewModel();
            transferViewModel = await _paymentSvc.TransferBetweenAccounts(vm);

            if (transferViewModel.HasError)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


    }
}
