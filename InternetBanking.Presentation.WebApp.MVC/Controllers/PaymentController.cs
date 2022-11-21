using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.ViewModels.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Presentation.WebApp.MVC.Controllers
{
    [Authorize(Roles = "Client")]

    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> PaymentExpress()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentExpress(PaymentsSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("PaymentExpress", vm);
            }


            return RedirectToRoute(new { controller = "Payment", action = "Index" });
        }


        public async Task<IActionResult> PaymentCreditCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentCreditCard(PaymentsSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("PaymentCreditCard", vm);
            }


            return RedirectToRoute(new { controller = "Payment", action = "Index" });
        }

        public async Task<IActionResult> PaymentLoan()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PaymentLoan(PaymentsSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("PaymentLoan", vm);
            }


            return RedirectToRoute(new { controller = "Payment", action = "Index" });
        }

        public async Task<IActionResult> PaymentBeneficiary()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PaymentBeneficiary(PaymentsSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("PaymentBeneficiary", vm);
            }


            return RedirectToRoute(new { controller = "Payment", action = "Index" });
        }
    }
}
