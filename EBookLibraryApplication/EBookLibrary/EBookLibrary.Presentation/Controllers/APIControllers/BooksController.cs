using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class BooksController : ControllerBase
    {
        private readonly IBookServices _bookservices;
        public BooksController(IBookServices bookservices)
        {
            _bookservices = bookservices;

        }

        [HttpPost]
        [Route("get-book-by-name")]
        public IActionResult GetBook(string Id)
        {
            var response = _bookservices.FindBook(Id);
            return Ok(response);
           
        }
    }
}
