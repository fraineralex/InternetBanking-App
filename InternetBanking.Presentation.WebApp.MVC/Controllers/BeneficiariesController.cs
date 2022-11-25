using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.BeneficiarySaveViewModel;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Controllers
{
    public class BeneficiariesController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse userViewModel;

        public BeneficiariesController(IHttpContextAccessor httpContextAccessor , IBeneficiaryService beneficiaryService, IProductService productService, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _beneficiaryService = beneficiaryService;
            _productService = productService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var beneficiaries = await _beneficiaryService.GetAllViewModel();
            beneficiaries = beneficiaries.Where(r => r.UserId == userViewModel.Id).ToList();

            ViewBag.Recipients = beneficiaries;
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _beneficiaryService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecipient(BeneficiarySaveViewModel vm)
        {
            await _beneficiaryService.Delete(vm.Id);
            return RedirectToRoute(new { controller = "Beneficiaries", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(BeneficiarySaveViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            
            var beneficiaries = await _beneficiaryService.GetAllViewModel();
            var products = await _productService.GetAllViewModel();

            var savingAccount = products.Where(e => e.AccountNumber == vm.BeneficiaryAccountNumber).FirstOrDefault();
            beneficiaries = beneficiaries.Where(e => e.UserId == userViewModel.Id).ToList();
            ViewBag.Recipients = beneficiaries;


            if (!ModelState.IsValid && vm.BeneficiaryAccountNumber == null)
            {
                return View(vm);
            }

            if (!await _productService.ExistCodeNumber(vm.BeneficiaryAccountNumber))
            {
                ModelState.AddModelError("AccountValidation", "The account number entered is not correct");
                return View("Index", vm);
            }

            if (savingAccount.TypeAccountId != (int)AccountTypes.SavingAccount)
            {
                ModelState.AddModelError("AccountValidation", "The account number entered is not a Saving Account");
                return View("Index", vm);
            }

            if (savingAccount.ClientId == userViewModel.Id)
            {
                ModelState.AddModelError("AccountValidation", "You cannot add yourself as a beneficiary");
                return View("Index", vm);
            }

            var beneficiaryViewModel = beneficiaries
                .Any(e => e.BeneficiaryAccountNumber == vm.BeneficiaryAccountNumber && e.UserId == vm.UserId);

            if (beneficiaryViewModel)
            {
                ModelState.AddModelError("AccountValidation", "You have a beneficiary with this number account");
                return View("Index", vm);
            }

            var beneficiary = await _userService.GetUserById(savingAccount.ClientId);
            vm.BeneficiaryName = $"{beneficiary.FirstName} {beneficiary.LastName}";

            BeneficiarySaveViewModel beneficiaryVm = await _beneficiaryService.Add(vm);
            return RedirectToRoute(new { controller = "Beneficiaries", action = "Index" }) ;
        }
    }
}
