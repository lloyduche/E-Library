using EBookLibrary.Client.Core.Implementations;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.ViewModels.BookVMs;
using Microsoft.AspNetCore.Http;
using EBookLibrary.ViewModels.ReviewVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> BookDetail(string id)
        {
            var response = await _book.GetBook(id);
            /*if (response.Success is true)
            {
                return View(response.data);
            }
            return BadRequest();*/
            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddBook model)
        {
            var response = await _book.Add(model);
            if (response.Success is true)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBook(string id)
        {
            var data = await _book.GetBook(id);
             return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBooks(UpdateBookViewModel model, string Id)
        {
            if (ModelState.IsValid)
            {
                var response = await _book.UpdateBook(model,Id);
                if (response.Successful is true)
                {
                    ViewBag.Message = "Update Successful";
                    return View("successReg");
                }
            }
            return BadRequest();

        }


        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(UpdateBookViewModel model)
        {
            var uploadphotodtovm = new UploadPhotoVM
            {
                BookId = model.Id,
                BookPhoto = model.UploadPhotoVM.BookPhoto

            };

           var response = await _book.UploadPhoto(uploadphotodtovm);
            if (response)
            {
                return RedirectToAction("UpdateBook", new { id = model.Id });
            }

            return BadRequest();

        }

        [HttpPost]
        public async Task<IActionResult> AddReview(GetBookDetailsResponseVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Wrong Review Format");
                return RedirectToAction("BookDetail", new {id = model.Id });

            }

            var reviewdto = new AddReviewDto
            {
                Comment = model.AddReviewVM.Comment,
                BookId = model.Id,
                UserId = "626751aa-e8ce-44e6-b4de-0d4f95010c37"
            };

            var res = await _book.AddReview(reviewdto);
            if (!res)
            {
                ModelState.AddModelError("", "Wrong Review Format");
                return RedirectToAction("BookDetail", new { id = model.Id });
            }

            

            return RedirectToAction("BookDetail", new { id=model.Id}) ;
        }
        [HttpPost]
        public async Task<IActionResult> AddRating(GetBookDetailsResponseVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Wrong Rating Format");
                return RedirectToAction("BookDetail", new { id = model.Id });

            }

            var ratingdto = new AddRatingDto
            {
                Ratings = model.AddRatingVM.Ratings,
                BookId = model.Id,
                UserId = "626751aa-e8ce-44e6-b4de-0d4f95010c37"
            };

            var res = await _book.AddRating(ratingdto);
            if (!res)
            {
                ModelState.AddModelError("", "Wrong Review Format");
                return RedirectToAction("BookDetail", new { id = model.Id });
            }


            
            return RedirectToAction("BookDetail", new { id = model.Id });
        }

        /*public async Task<IActionResult> UploadPhoto([FromForm] )
        {
            
        }*/ 
    }
}
