using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedUser()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Naruto123!");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }


            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Naruto123!");

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
        public async Task SeedRole()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole("User");
                role.NormalizedName = "USER";
                await _roleManager.CreateAsync(role);
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole("Admin");
                role.NormalizedName = "USER";
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
