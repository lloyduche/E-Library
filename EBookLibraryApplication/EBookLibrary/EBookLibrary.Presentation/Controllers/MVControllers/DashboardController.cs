using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageBooks()
        {
            return View();
        }
    }
}
