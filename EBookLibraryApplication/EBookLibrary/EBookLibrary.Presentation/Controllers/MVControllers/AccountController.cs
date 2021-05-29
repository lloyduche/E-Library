using EBookLibrary.Models;
using EBookLibrary.Presentation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: AuthenticationController
        public ActionResult Index()
        {
            return View();
        }

        // Register user
        [HttpPost]
        public async Task<ActionResult> Register(RegisterationViewModel model)
        {
            var user = _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Gender = model.Gender,
                    //AvatarUrl = model.AvatarFile
                };

                var res = await _userManager.CreateAsync(newUser, model.Password);
                await _userManager.AddToRoleAsync(newUser, "Regular");

                if (res.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    ViewBag.Message = "Account created successfully";

                    return RedirectToAction("Index", "Home");
                }
            }
            return Ok("User already exist");

        }
    }
}
