using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pilgrimsvandringen_grupp_5_e.Data;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Repositories;
using pilgrimsvandringen_grupp_5_e.Services;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using pilgrimsvandringen_grupp_5_e.ViewModels;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace pilgrimsvandringen_grupp_5_e.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IMessageService _messageService;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext appDbContext, IAccountService accountService, IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = appDbContext;
            _accountService = accountService;
            _messageService = messageService;
        }      
        
        /// <summary>
        /// to get user by aspnetuseridentity guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<User> GetUserByGuid(string guid)
        {            
            return await _context.Users.FirstOrDefaultAsync(x => x.IdentityUserId == guid);
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);

            if (result.Succeeded)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await GetUserByGuid(userId);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Account", user.UserName);
            }

            ModelState.AddModelError("", "Felaktig inloggning");
            return View(loginViewModel);
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = new IdentityUser { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                User registerUser = new User { UserName = user.UserName, RoleId = 2, IdentityUserId = user.Id}; // TODO fetch RoleId from database
                await _accountService.CreateUserAsync(registerUser);
                await _signInManager.SignInAsync(user, false);
                HttpContext.Session.SetString("UserId", registerUser.Id.ToString());

                return RedirectToAction("Index", "Account", registerUser.UserName);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(registerViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            HttpContext.Session.SetString("UserId", "-1");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null && userId != "-1")
            {
                var userInt = int.Parse(userId);
                var user = await _messageService.GetUserByIdAsync(userInt);
                ViewBag.UserName = user.UserName;
            }
            
            return View();
        }
                
        public IActionResult AccessDenied() => View();
    }


}
