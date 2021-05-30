using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }




        public IActionResult PasswordReset()
        {
            return View();
        }
    }
}
