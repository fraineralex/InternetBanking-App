using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApp.InternetBanking.Middlewares;

namespace InternetBanking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _producService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HomeController(IUserService userService, RoleManager<IdentityRole> roleManager, IProductService productService, IHttpContextAccessor httpContextAccesor)
        {
            _userService = userService;
            _roleManager = roleManager;
            _producService = productService;
            _httpContextAccessor = httpContextAccesor;
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        public async Task<IActionResult> HomeAdmin()
        {
            //LEONARDO, si esto le da error vaya al ProductService y ponga el nombre de su servidor en el connectionString
            ViewBag.clientStatus = await _producService.GetClientStatus();
            ViewBag.clientProduct = await _producService.GetClientProducts();
            ViewBag.clientpayment = await _producService.GetPaymentQuantities();
            ViewBag.transacctions = await _producService.GetTransacctions();
            return View();
        }

        [ServiceFilter(typeof(ClientAuthorize))]
        public async Task<IActionResult> HomeClient()
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            ViewBag.SavingsAccounts = await _producService.GetAllProductByUser(user.Id, (int)AccountTypes.SavingAccount);
            ViewBag.CreditCards = await _producService.GetAllProductByUser(user.Id, (int)AccountTypes.CreditAccount);
            ViewBag.Loans = await _producService.GetAllProductByUser(user.Id, (int)AccountTypes.LoanAccount);

            return View();
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
        public async Task<IActionResult> UserManagement()
        {
            ViewBag.Users = await _userService.GetAllUsers();
            return View();
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        public  async Task<IActionResult> Register()
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            return View(new UserSaveViewModel());
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Home", action = "UserManagement" });
        }
        [ServiceFilter(typeof(AdminAuthorize))]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = HttpContext.Session.Get<AuthenticationResponse>("user");

            if (id == user.Id)
            {
                return RedirectToRoute(new {controller ="Home", action = "UserManagement" });
            }
            UserSaveViewModel vm = await _userService.GetUserById(id);
            return View("Register", vm);
        }
        [ServiceFilter(typeof(AdminAuthorize))]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserSaveViewModel vm)
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View("Register", vm);
            }
            await _userService.UpdateUserAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "UserManagement" });
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        public async Task<IActionResult> ActiveUser(string id)
        {
            return View("ActiveUser", await _userService.GetUserById(id));
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ActiveUser(UserSaveViewModel vm)
        {
            await _userService.ActivedUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "UserManagement" });
        }
    }
}
