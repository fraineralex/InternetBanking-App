using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Application.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IRecipientService _beneficiaryService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse currentlyUser;

        public PaymentController(IHttpContextAccessor httPContextAccesor , IPaymentService paymentService, IProductService productService, IUserService userService, IRecipientService beneficiaryService)
        {
            _httpContextAccessor = httPContextAccesor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _paymentService = paymentService;
            _productService = productService;
            _userService = userService;
            _beneficiaryService = beneficiaryService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ExpressPayment()
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ExpressPayment(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            vm.TypeOfPayment = 0;
            if (!ModelState.IsValid)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                return View(vm);
            }

            ProductViewModel accountToPay = await _productService.GetProductByNumberAccountForPayment(vm.OriginAccountNumber, vm.Amount);

            if (accountToPay.HasError)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                vm.HasError = accountToPay.HasError;
                vm.Error = accountToPay.Error;
                return View(vm);
            }
            
            ProductViewModel destinationAccount = await _productService.GetProductByNumberAccountForPayment(vm.DestinationAccountNumber);

            if (destinationAccount.HasError)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                vm.HasError = true;
                vm.Error = "Account Destination couldn't be found";
                return View(vm);
            }

            if (destinationAccount.TypeAccountId != (int)AccountTypes.SavingAccount)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                vm.HasError = true;
                vm.Error = "Debe digitar el numero de una cuenta de ahorros";
                return View(vm);
            }

            if (vm.OriginAccountNumber == vm.DestinationAccountNumber)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                vm.HasError = true;
                vm.Error = "No puedes hacerte autotrasferencias en este apartado, ve a la sección de transferencias";
                return View(vm);
            }

            var owner = await _userService.GetUserById(accountToPay.ClientId);
            vm.Owner = $"{owner.FirstName} {owner.LastName}";
            return RedirectToAction("ConfirmPayment", vm);
        }

        public async Task<ActionResult> CreditPayment()
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
            ViewBag.CreditCards = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.CreditAccount);
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreditPayment(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            vm.TypeOfPayment = 0;
            if (!ModelState.IsValid)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                ViewBag.CreditCards = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.CreditAccount);
                return View(vm);
            }

            ProductViewModel accountToPay = await
                _productService.GetProductByNumberAccountForPayment
                (vm.OriginAccountNumber, vm.Amount);

            if (accountToPay.HasError)
            {
                vm.HasError = accountToPay.HasError;
                vm.Error = accountToPay.Error;
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                ViewBag.CreditCards = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.CreditAccount);
                return View(vm);
            }

            ProductViewModel destinationAccount = await
                _productService.GetProductByNumberAccountForPayment
                (vm.DestinationAccountNumber);

            if(vm.Amount > destinationAccount.Discharge)
            {
                vm.Amount = destinationAccount.Discharge;
            }

            var owner = await _userService.GetUserById(accountToPay.ClientId);
            vm.Owner = $"{owner.FirstName} {owner.LastName}";
            return RedirectToAction("ConfirmCreditCardPayment", vm);
        }

        public async Task<ActionResult> LoamPayment()
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
            ViewBag.Loans = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.LoanAccount);
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoamPayment(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }


            vm.TypeOfPayment = 1;
            if (!ModelState.IsValid)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                ViewBag.Loans = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.LoanAccount);
                return View(vm);
            }

            ProductViewModel accountToPay = await
                _productService.GetProductByNumberAccountForPayment
                (vm.OriginAccountNumber, vm.Amount);

            if (accountToPay.HasError)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                ViewBag.Loans = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.LoanAccount);
                vm.HasError = accountToPay.HasError;
                vm.Error = accountToPay.Error;
                return View(vm);
            }

            
            ProductViewModel destinationAccount = await _productService.GetProductByNumberAccountForPayment(vm.DestinationAccountNumber);

            if (vm.Amount > destinationAccount.Charge)
            {
                vm.Amount = destinationAccount.Charge;
            }

            var owner = await _userService.GetUserById(accountToPay.ClientId);
            vm.Owner = $"{owner.FirstName} {owner.LastName}";
            return RedirectToAction("ConfirmPayment", vm);
        }

        public async Task<ActionResult> BeneficiaryPayment()
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }


            ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
            var payments = await _beneficiaryService.GetAllViewModel();
            ViewBag.Beneficiaries = payments.Where(b => b.UserId == currentlyUser.Id).ToList();

            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> BeneficiaryPayment(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }


            vm.TypeOfPayment = 0;
            if (!ModelState.IsValid)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                var payments = await _beneficiaryService.GetAllViewModel();
                ViewBag.Beneficiaries = payments.Where(b => b.UserId == currentlyUser.Id).ToList();
                return View(vm);
            }

            ProductViewModel accountToPay = await
                _productService.GetProductByNumberAccountForPayment
                (vm.OriginAccountNumber, vm.Amount);

            if (accountToPay.HasError)
            {
                ViewBag.SavingsAccounts = await _productService.GetAllProductByUser(currentlyUser.Id, (int)AccountTypes.SavingAccount);
                var payments = await _beneficiaryService.GetAllViewModel();
                ViewBag.Beneficiaries = payments.Where(b => b.UserId == currentlyUser.Id).ToList();
                vm.HasError = accountToPay.HasError;
                vm.Error = accountToPay.Error;
                return View(vm);
            }

            var owner = await _userService.GetUserById(accountToPay.ClientId);
            vm.Owner = $"{owner.FirstName} {owner.LastName}";
            return RedirectToAction("ConfirmPayment", vm);
        }

        public ActionResult ConfirmPayment(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPaymentPost(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });

            }
            await _paymentService.Payment(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public ActionResult ConfirmCreditCardPayment(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCreditCardPaymentPost(SavePaymentViewModel vm)
        {
            if (currentlyUser.Roles.FirstOrDefault() == "Admin")
            {
                return RedirectToRoute(new { controller = "Home", action = "DashboardAdmin" });
            }

            await _paymentService.CreditCardPayment(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
