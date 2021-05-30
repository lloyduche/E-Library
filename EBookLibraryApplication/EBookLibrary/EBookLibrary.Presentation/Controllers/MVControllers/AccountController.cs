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
        private readonly IAuthenticationService _auth;
        public AccountController(IAuthenticationService authenticationService)
        {
            _auth = authenticationService;
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult successReg()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterationViewModel model)
        {
           var response = await _auth.Register(model);
            if (response.Successful is true)
            {
                return RedirectToAction("successReg");
            }
           return BadRequest();
        }
    }
}
