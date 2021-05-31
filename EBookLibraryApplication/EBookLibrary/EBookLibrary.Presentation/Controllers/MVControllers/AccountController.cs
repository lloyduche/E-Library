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

        public IActionResult PasswordForgot()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PasswordReset(string email, string token)
        {
            var passviewmodel = new PasswordResetViewModel
            {
                Email = email,
                token = token
            };
            return View(passviewmodel);
        }



        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var response = await _auth.ForgotPassword(model);
            if (response.Successful is true)
            {
                ViewBag.Title = "Forgot Password";
                ViewBag.Message = "A Password Reset Link Has Been Sent To Your Mail";
                return RedirectToAction("successReg");
            }
            return BadRequest();

        }






        [HttpPost]
        public async Task<ActionResult> Register(RegisterationViewModel model)
        {
           var response = await _auth.Register(model);
            if (response.Successful is true)
            {
                ViewBag.Title = "Registration";
                ViewBag.Message = "Registration Successful";
                return RedirectToAction("successReg");
            }
           return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> PasswordReset(PasswordResetViewModel model)
        {
            var response = await _auth.ResetPassword(model);
            if (response.Successful is true)
            {
                ViewBag.Title = "Password Reset";
                ViewBag.Message = "Password Reset Successful";
                return RedirectToAction("successReg");
            }
            return BadRequest();

        }






    }
}
