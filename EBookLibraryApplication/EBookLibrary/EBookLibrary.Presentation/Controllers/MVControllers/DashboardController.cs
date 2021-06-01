using EBookLibrary.ViewModels.UserVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBookLibrary.Client.Core;
using EBookLibrary.Client.Core.Abstractions;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class DashboardController : Controller
    {
        private readonly IClientUserService _userService;

        public DashboardController(IClientUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserById("");

            if(user.Data.AvatarUrl == null)
            {
                user.Data.AvatarUrl = "~/images/dummyman.jpg";
            }

            return View(user.Data);
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
