using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class BooksController : BaseAPIController
    {
        private readonly IGenericRepository<Book> _bookRepo;

        private readonly IBookServices _bookservices;

        public BooksController(IBookServices bookservices, IGenericRepository<Book> bookRepo)
        {
            _bookservices = bookservices;
            _bookRepo = bookRepo;
        }

        [HttpPost]
        [Route("get-book-by-name")]
        public async Task<IActionResult> GetBook([FromBody] string Id)
        {
            var response = await _bookservices.FindBook(Id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<PagedResult<Book>> GetAllBooks(int pageNumber, int numberToReturn)
        {
            return await _bookRepo.GetByPage(pageNumber, numberToReturn);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search(SearchTermDto term)
        {
            var response = await _bookservices.GetAllBooksWhere(term);
            return Ok(response);
        }
    }
}
