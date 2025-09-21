using ECommerce.Models;
using ECommerce.View_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> _userManager, 
            SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
         
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser applicationUser = await userManager.FindByNameAsync(loginVM.UserName);

                if (applicationUser != null)
                {
                    if (applicationUser.LockoutEnd != null)
                        return View("LockedPage");

                    bool isFound = await userManager.CheckPasswordAsync(applicationUser, loginVM.Password);

                    if (isFound)
                    {
                        await signInManager.SignInAsync(applicationUser, loginVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid User name or Password");
            }
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = registerVM.UserName,
                    Email = registerVM.Email,
                    PasswordHash = registerVM.Password,
                    Address = registerVM.Address
                };

                IdentityResult result = await userManager.CreateAsync(applicationUser, registerVM.Password);

                if (result.Succeeded) 
                {
                    await signInManager.SignInAsync(applicationUser, false);

                    return RedirectToAction("Index", "Home", new {page=1});
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            
            return View(registerVM);
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
