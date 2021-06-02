using EBookLibrary.ViewModels.UserVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBookLibrary.Client.Core;
using EBookLibrary.Client.Core.Abstractions;
using EBookLibrary.ViewModels;
using EBookLibrary.Client.Core.Implementations;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class DashboardController : Controller
    {
        private readonly IClientUserService _userService;
        private readonly IClientBookService _bookService;

        public DashboardController(IClientUserService userService, IClientBookService bookService)
        {
            _userService = userService;
            _bookService = bookService;
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
        public async Task<IActionResult> Admin()
        {
            var totalNumOfUsers = await _userService.GetUsersCount();

            var totalNumOfBooks = await _bookService.GetBooksCount();

            var totalNumOfReviews = await _bookService.GetReviewsCount();

            var adminDashboardViewModel = new AdminDashboardViewModel
            {
                RegisteredUsers = totalNumOfUsers.Data,
                TotalBooks = totalNumOfBooks.Data,
                RecommendedReviews = totalNumOfReviews.Data
            };

            return View(adminDashboardViewModel);
        }
    }
}
