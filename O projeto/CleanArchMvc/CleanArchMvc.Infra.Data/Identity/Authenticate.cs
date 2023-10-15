using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class Authenticate : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Authenticate(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            return result.Succeeded;
        }

        public async Task<bool> Register(string email, string password)
        {
            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, false);
            }

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
