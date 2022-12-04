using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppNerdAlert.Data;
using WebAppNerdAlert.Models;
using WebAppNerdAlert.ViewModels;

namespace WebAppNerdAlert.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginViewModel)
        {
            if (!ModelState.IsValid) return View(LoginViewModel);

            var user = await _userManager.FindByEmailAsync(LoginViewModel.Username);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, LoginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, LoginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Hobby");
                    }
                }
                TempData["Error"] = "Wrong Email or Password";
                return View(LoginViewModel);
            }
            TempData["Error"] = "Wrong Email or Password";
            return View(LoginViewModel);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Username);
            if(user != null)
            {
                TempData["Error"] = "This Email Address is Aleady In Use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.Username,
                UserName = registerViewModel.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if(newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Index", "Hobby");   
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Hobby");
        }
    }
}
