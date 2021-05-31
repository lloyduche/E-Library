using EBookLibrary.Client.Core.Abstractions;
using EBookLibrary.ViewModels.BookVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.MVControllers
{
    public class BooksController : Controller
    {

        private readonly IBookService _books;

        public BooksController(IBookService books)
        {
            _books = books;
        }


        [HttpGet]
        public IActionResult UpdateBook()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBook(UpdateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _books.UpdateBook(model);
                if (response.Successful is true)
                {
                    ViewBag.Message = "Update Successful";
                    return RedirectToAction("successReg");
                }
            }
                            return BadRequest();

        }
    }
}
