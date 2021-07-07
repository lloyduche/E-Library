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
using Microsoft.AspNetCore.Http;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("Id");
            if (id == null) return Redirect("/account/login");
            var user = await _userService.GetUserById(id);

            if(user.Data.AvatarUrl == null)
            {
                user.Data.AvatarUrl = "~/images/dummyman.jpg";
            }

            return View(user.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Admin(string id)
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

        [HttpPost]
        public ActionResult UploadAvatar([FromForm] IFormFile avatar)
        {
            var Id = HttpContext.Session.GetString("Id");
            if (Id == null) return Redirect("/account/login");
            var model = new UploadUserAvatarViewModel
            {
                Avatar = avatar,
                UserId = Id
            };
            _userService.UploadPhoto(model);
            return RedirectToAction("Index");
        }
    }
}
