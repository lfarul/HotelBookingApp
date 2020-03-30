using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.DataContext;
using HotelBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class AdministrationController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }


        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        // Usuwam Usera i przekazuje w metodzie id usera. Używając tego przychodzącego id chce uzyskać określonego usera z bazy danych
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // znajdź usera po Id
            var user = await userManager.FindByIdAsync(id);

            // jeżeli nie ma usera
            if (user == null)
            {
                ViewBag.Errormessage = $"User z ID = {id} nie został znaleziony.";
                return View("NotFound");
            }

            // w przeciwnym razie korzystam z servicu userManager i wywołuje metodę DeleteAsync w której przekazuje usera do usunięcia.
            else
            {
                var result = await userManager.DeleteAsync(user);

                // jeżeli udało się uzunąć użytkownika
                if (result.Succeeded)
                {
                    //wróc do widoku ListUsers
                    return RedirectToAction("ListUsers");
                }

                // dla każdego errora w result podczas usuwania usera
                foreach (var error in result.Errors)
                {
                    // dodaje errory do ModelState
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListUsers");
            }
        }
    }
}