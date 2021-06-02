using EBookLibrary.Client.Core.Abstractions;
using EBookLibrary.ViewModels.UserVMs;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationViewModel model)
        {
           var response = await _auth.Register(model);
            if (response.Successful is true)
            {
                return RedirectToAction(nameof(SuccessReg));
            }
           return BadRequest();
        }


        public IActionResult SuccessReg()
        {
            return View();
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
            var response = await _auth.UpdateUser(model);
            if (response is true)
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var response = await _auth.Login(model);
            if (response.Success)
            {
                HttpContext.Session.SetString("access_token", response.Data);
                return RedirectToAction("Index","Dashboard", new {Id = "6f07f58e-d3af-465f-8a75-62786e8179a3" });
            }
            ModelState.AddModelError("", response.Message);
            return View(model);
        }


        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }


    }
}
