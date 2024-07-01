using CleanArch.ATG.Domain.Entities.Identity;
using CleanArch.ATG.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Infrastructure
{
    public static class DataSeeding
    {
        public static async void SeedData( IConfiguration _configuration , UserManager<UserApplication> _userManager , RoleManager<AppRole> _roleManager )
        {
            if (_userManager.Users.Count() == 0)
            {
                string userName = _configuration ["SuperAdminCredentials:userName"];
                string Password = _configuration ["SuperAdminCredentials:Password"];
                var superAdmin = new UserApplication()
                {
                    UserName = userName ,
                    Password = Password
                };
                var result = await _userManager.CreateAsync(superAdmin , Password);
                AppRole superAdminRole = await _roleManager.FindByNameAsync("SuperAdmin");
                if (superAdminRole == null)
                {
                    superAdminRole = new AppRole()
                    {
                        Name = "SuperAdmin"
                    };
                }
                await _roleManager.CreateAsync(superAdminRole);
                await _userManager.AddToRoleAsync(superAdmin , "SuperAdmin");

            }
        }
    }
}
