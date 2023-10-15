using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticate;

        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var result = await _authenticate.Login(login.Email, login.Password);

            if (result)
            { 
                if(login.ReturnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(login.ReturnUrl);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(login);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authenticate.Register(model.Email, model.Password);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
           await _authenticate.Logout();
            return Redirect("/");
        }
    }
}
