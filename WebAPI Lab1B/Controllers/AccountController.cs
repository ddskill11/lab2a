// I, David Scott, student number 000358671, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Lab1B.Data;
using WebAPI_Lab1B.Models;

namespace WebAPI_Lab1B.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> RoleManager)
        {
            _userManager = userManager;
            _roleManager = RoleManager;
        }
        public async Task<IActionResult> SeedRoles()
        {
            ApplicationUser user1 = new ApplicationUser
            {
                Email = "John@doe.com",
                UserName = "John@doe.com",
                FirstName = "John",
                LastName = "Doe",
            };
            ApplicationUser user2 = new ApplicationUser
            {
                Email = "Jane@doe.com",
                UserName = "Jane@doe.com",
                FirstName = "Jane",
                LastName = "Doe",
            };
            IdentityResult result = await _userManager.CreateAsync(user1, "Mohawk1!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _userManager.CreateAsync(user2, "Mohawk1!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _roleManager.CreateAsync(new IdentityRole("Employee"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });

            result = await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });


            result = await _userManager.AddToRoleAsync(user1, "Employee");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });

            result = await _userManager.AddToRoleAsync(user2, "Supervisor");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });

            return RedirectToAction("Index", "Home");

        }

    }
}