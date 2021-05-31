using EBookLibrary.Models;
using EBookLibrary.ViewModels.UserVMs;
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
        private readonly List<User> _users;
        private readonly IAuthenticationService _auth;
        public AccountController(IAuthenticationService authenticationService)
        {
            _auth = authenticationService;
            _users = new List<User>
            {
                new User { Id="1", FirstName="John", LastName="Doe", Email = "johndoe@gmail.com"},
                new User { Id="2", FirstName="A", LastName="B", Email = "firstname@gmail.com"},
                new User { Id="3", FirstName="C", LastName="D", Email = "jamesjohn@gmail.com"},
                new User { Id="4", FirstName="E", LastName="F", Email = "abchdjfk@gmail.com"}
            };
        }
        
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        public async Task<ActionResult> Register(Users model)
        {
           var response = await _auth.Register(model);
            if (response.Success is true)
            {
                return RedirectToAction("successReg");
            }
           return BadRequest();
        }
        public ActionResult successReg()
        {
            return View();
        }
        [HttpPost]
        
        [HttpGet]
        public ActionResult Update(string email, string firstname, string lastname, string gender)
        {
            var update = new UpdateViewModel
            {
                Email = email,
                FirstName = firstname,
                LastName = lastname,
                Gender = gender
            };
            return View(update);
        }
        [HttpPost]
        public async Task<ActionResult> Update(UpdateViewModel model)
        {
            var response = await _auth.Update(model);
            if (response.Successful is true)
                return Redirect("dashboard");
            return BadRequest();
        }

        public ActionResult Delete()
        {
            return View(_users);
        }

        [HttpPost]
        public ActionResult DeleteUser()
        {
            
            return View();
        }

    }
}
