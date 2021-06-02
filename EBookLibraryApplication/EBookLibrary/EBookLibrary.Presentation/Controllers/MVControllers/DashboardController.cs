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
        private readonly IClientBookService _clientBookService;

        public DashboardController(IClientUserService userService,IClientBookService clientBookService)
        {
            _userService = userService;
            _clientBookService = clientBookService;
            
        }
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userService.GetUserById(id);

            if(user.Data.AvatarUrl == null)
            {
                user.Data.AvatarUrl = "~/images/dummyman.jpg";
            }

            return View(user.Data);
        }
        public async Task<IActionResult> Admin()
        {
            var totalNumOfUsers = await _userService.GetUsersCount();

            var totalNumOfBooks = await _clientBookService.GetBooksCount();

            var totalNumOfReviews = await _clientBookService.GetReviewsCount();

            var adminDashboardViewModel = new AdminDashboardViewModel
            {
                RegisteredUsers = totalNumOfUsers.Data,
                TotalBooks = totalNumOfBooks.Data,
                RecommendedReviews = totalNumOfReviews.Data
            };

            return View(adminDashboardViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ManageAccount(int PageNumber = 1, int PageSize = 5)
        {
            var model = new SearchParametersViewModel
            {
                PageNumber = PageNumber,
                PageSize = PageSize
            };
            var myUsers = await _userService.GetAllUser(model);

            return View(myUsers);
        }

        [HttpGet]
        public async Task<IActionResult> ManageBooks(int PageNumber =1, int PageSize = 5)
        {
            var model = new SearchParametersViewModel
            {
                PageNumber =PageNumber,
                PageSize = PageSize
            };
            var myBook = await  _clientBookService.Books(model);
            return View(myBook);
        }
    }
}
