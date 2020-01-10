using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("account")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        /// <summary>
        /// 
        /// </summary>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            return View(new Login()
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Login(Login loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Name);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var signInResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                }
            }
            ModelState.AddModelError("", "Username or password is invalid!");
            return View(loginModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}