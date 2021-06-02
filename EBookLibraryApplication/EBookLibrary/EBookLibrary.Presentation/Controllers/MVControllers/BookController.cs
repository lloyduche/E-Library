using EBookLibrary.Client.Core.Implementations;
using EBookLibrary.ViewModels.BookVMs;
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
            return View();
        }

        [HttpPost("add-book-view")]
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
    }
}
