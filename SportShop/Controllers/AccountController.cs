using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Models.ViewModels;

namespace SportShop.Controllers
{
    /// <summary>
    /// Controller which manages user session.
    /// </summary>
    [Authorize]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Method which redirect user to login page.
        /// </summary>
        /// <param name="returnUrl"></param>
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.CurrentPage = "Login";
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Method which allows user to login.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.Name);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var signInResult =
                        await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return Redirect(loginViewModel?.ReturnUrl ?? "/admin/products");
                    }
                }
                TempData["Message"] = "Username or password is invalid!";
                return View(loginViewModel);
            }
            else
            {
                return View(loginViewModel);
            }

        }

        /// <summary>
        /// Method which logs user out and redirects to given returnUrl.
        /// </summary>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}