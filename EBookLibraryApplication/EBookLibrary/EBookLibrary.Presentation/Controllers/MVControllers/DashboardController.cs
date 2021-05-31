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
            var user = await _userService.GetUserById("0af0a8f9-5c24-461e-89ab-24883356170a");

            if(user.Data.AvatarUrl == null)
            {
                user.Data.AvatarUrl = "~/images/dummyman.jpg";
            }

            return View(user.Data);
        }
    }
}
