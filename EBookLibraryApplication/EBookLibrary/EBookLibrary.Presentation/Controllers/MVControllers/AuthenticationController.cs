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
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AuthenticationController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        // GET: AuthenticationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AuthenticationController/Details/5
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
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Regular");
                    return Ok("Registered successfully.");
                }
            }
            return BadRequest("User already exist");
        }
    }
}
