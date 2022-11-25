using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Application.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles = "Client")]
    public class CashAdvanceController : Controller
    {
        private readonly IPaymentService _paymentSvc;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse currentlyUser;

        public CashAdvanceController(IHttpContextAccessor httPContextAccesor , IPaymentService paymentService, IProductService productService, IUserService userService)
        {
            _httpContextAccessor = httPContextAccesor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _paymentSvc = paymentService;
            _productService = productService;
            _userService = userService;
        }
        
        public async Task<ActionResult> Index()
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "HomeAdmin" });
            }

            ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
            ViewBag.CreditCards = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.CreditAccount);
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "HomeAdmin" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                ViewBag.CreditCards = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.CreditAccount);
                return View(vm);
            }

            ProductViewModel creditCardOrigin = await
                _productService.GetProductByNumberAccountForPayment
                (vm.OriginAccountNumber, vm.Amount);

            if (creditCardOrigin.HasError)
            {
                vm.HasError = true;
                vm.Error = "You just can withdraw an amount of money that you have, you cannot withdraw an amount of money that you don't have in the credit card";
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                ViewBag.CreditCards = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.CreditAccount);
                return View(vm);
            }

            var owner = await _userService.GetUserById(creditCardOrigin.ClientId);
            vm.Owner = $"{owner.FirstName} {owner.LastName}";

            await _paymentSvc.CashAdvance(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
