using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Security.Claims;

namespace PartyInvites.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly SecurityDbContext _securityDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            SecurityDbContext securityDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _securityDbContext = securityDbContext;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        /*        [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Register(LoginModel registerModel)
                {
                    if (ModelState.IsValid)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                        var user = new IdentityUser(registerModel.Name);
                        var registerResult = await _userManager.CreateAsync(user, registerModel.Password);

                        if (registerResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                    }


                    ModelState.AddModelError("", "Enter a name and password");
                    return View(registerModel);
                }*/

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(LoginViewModel loginVM)
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "Already signed in.");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new IdentityUser(loginVM.Name);
            var registerResult = await _userManager.CreateAsync(user, loginVM.Password);

            if (registerResult.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("UserType", "user"));

                return RedirectToAction(nameof(Login), new { returnUrl = "/Guest/" });
            }

            foreach (var error in registerResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl, });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user =
                    await _userManager.FindByNameAsync(loginVM.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user,
                        loginVM.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction(nameof(Details), loginVM);
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View();
        }

        public IActionResult Details(LoginViewModel loginVm)
        {

            if (User != null)
            {
                return View(loginVm);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            return View();
        }
    }
}
