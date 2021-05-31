using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EBookLibrary.ViewModels.UserVMs;
namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var userDetails = new UserDashboardViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "JD@gmail.com",
                AvatarUrl = "~/images/dummyman.jpg"
            };
            return View(userDetails);
        }
    }
}
