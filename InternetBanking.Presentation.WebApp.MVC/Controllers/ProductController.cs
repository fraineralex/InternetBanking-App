﻿using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ITypeAccountService _typeAccountService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse userSesion;

        public ProductController(IHttpContextAccessor httPContextAccesor, IProductService productService, IUserService userService, ITypeAccountService typeAccountService)
        {
            _httpContextAccessor = httPContextAccesor;
            userSesion = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _productService = productService;
            _userService = userService;
            _typeAccountService = typeAccountService;
        }
        
        public async Task<ActionResult> Index(string id)
        {
            List<ProductViewModel> productViewModelList = await _productService.GetAllProductWithInclude(id);
            var owner = await _userService.GetUserById(id);
            ViewBag.ClientId = owner.Id;
            foreach (var item in productViewModelList)
            {
                item.Owner = $"{owner.FirstName} {owner.LastName}";
            }

            ViewBag.ProductList = productViewModelList;
            return View(new ProductViewModel());
        }

        public async Task<ActionResult> CreateAccount(string id)
        {
            ViewBag.Types = await _typeAccountService.GetAllViewModel();
            ProductSaveViewModel vm = new();
            vm.ClientId = id;
            return View(vm);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAccount(ProductSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = await _typeAccountService.GetAllViewModel();
                return View(vm);
            }

            if (vm.TypeAccountId == (int)AccountTypes.SavingAccount)
            {
                await _productService.AddSavingAccountAsync(vm.ClientId, vm.Amount);
            }
            else if (vm.TypeAccountId == (int)AccountTypes.CreditAccount)
            {
                await _productService.CreateAccountAsync(vm.ClientId, vm.Amount, vm.TypeAccountId);
            }
            else
            {
                await _productService.CreateAccountAsync(vm.ClientId, vm.Amount, vm.TypeAccountId);
            }
            return RedirectToAction("Index", new { id = vm.ClientId });
        }

        public async Task<ActionResult> DeleteProduct(int id)
        {
            ProductViewModel vm =  await _productService.DeleteProductAsync(id);
            if (vm.HasError)
            {
                return View("DeleteProductMessageError", vm);
            }

            return RedirectToAction("Index", new { id = vm.ClientId });
        }
    }
}
