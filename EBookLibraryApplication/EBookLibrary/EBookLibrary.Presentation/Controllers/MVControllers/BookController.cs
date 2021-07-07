using EBookLibrary.Client.Core.Implementations;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.ViewModels;
using EBookLibrary.ViewModels.BookVMs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class BookController : Controller
    {
        private readonly IClientBookService _book;

        public BookController(IClientBookService book)
        {
            _book = book;
        }
        
        [HttpGet]
        public IActionResult AddBookView()
        {
            return View(new AddBook());
        }

        [HttpGet]
        public async Task<IActionResult> BookDetail(string id,  string message, bool? success)
        {
            var response = await _book.GetBook(id);
            if (!response.Success)
            {
                return RedirectToAction("ErrorHandler", "Error", new { statusCode = response.StatusCode});
            }
            /*if (response.Success is true)
            {
                return View(response.data);
            }
            return BadRequest();*/
            ViewBag.AddingSuccess = success;
            ViewBag.Message = message;
           
            return View(response.Data);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddBookView(AddBook model)
        {
            var response = await _book.Add(model);
            if (response.Success is true)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            return BadRequest();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMyBook(string id)
        {
            var data = await _book.GetBook(id);
            var model = new UpdateBookViewModel
            {
                Title = data.Data.Title,
                Author = data.Data.Author,
                Isbn = data.Data.Isbn,
                Pages =data.Data.Pages,
                Publisher = data.Data.Publisher,
                Category = data.Data.Category,
                Description = data.Data.Description,
                DatePublished = data.Data.DatePublished,
                Id = data.Data.Id,
                CopiesAvailable = data.Data.CopiesAvailable
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooks([FromForm] string bookid)
        {
            var data = await _book.DeleteBook(bookid);
            return Redirect("/Dashboard/ManageBooks");
        }




        [HttpPost]
        public async Task<IActionResult> UpdateBooks(UpdateBookViewModel model, string Id)
        {
            if (ModelState.IsValid)
            {
                var response = await _book.UpdateBook(model, Id);
                if (response.Successful is true)
                {
                    ViewBag.Message = "Update Successful";
                    return RedirectToAction("ManageBooks","Dashboard");
                }
            }
            return BadRequest();
        }

        public async Task<IActionResult> Search(SearchParametersViewModel1 model)
        {
            if (string.IsNullOrEmpty(model.Query)) return Redirect("/");
            ViewBag.query = model.Query;
            var resp = await _book.Search(model);
            return View(resp);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto( IFormFile avatar, string Id)
        {
            var uploadphotodtovm = new UploadPhotoVM
            {
                BookId = Id,
                BookPhoto = avatar
            };

            var response = await _book.UploadPhoto(uploadphotodtovm);
            if (response)
            {
                ViewBag.Message = "Update Successful";
                return View("successReg");
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(GetBookDetailsResponseVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Wrong Review Format");
                return RedirectToAction("BookDetail", new { id = model.Id });
            }

            var userId = HttpContext.Session.GetString("Id");

            var reviewdto = new AddReviewDto
            {
                Comment = model.AddReviewVM.Comment,
                BookId = model.Id,
                UserId = userId
            };

            
            var res = await _book.AddReview(reviewdto);
            var message = "Review Added successfully";
            var success = true;
            if (!res)
            {

                success = false;
                message = "There was an error adding the review";

            }

            return RedirectToAction("BookDetail", new { id = model.Id, Success = success, Message = message });
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(GetBookDetailsResponseVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Wrong Rating Format");
                return RedirectToAction("BookDetail", new { id = model.Id });
            }

            var userId = HttpContext.Session.GetString("Id");
            var ratingdto = new AddRatingDto
            {
                Ratings = model.AddRatingVM.Ratings,
                BookId = model.Id,
                UserId = userId
            };

            var res = await _book.AddRating(ratingdto);
            var message = "Rating Added Successfully";
            var success = true;

            if (!res)
            {
                //ModelState.AddModelError("", "Wrong Review Format");
                message = "There was an error adding the rating";
                success = false;
                //return RedirectToAction("BookDetail", new { id = model.Id });
            }
            
            return RedirectToAction("BookDetail", new { id = model.Id, Success = success, Message=message });
        }
    }
}
