using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.Models;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class BooksController : BaseAPIController
    {
        private readonly IGenericRepository<Book> _bookRepo;

        public BooksController(IGenericRepository<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public async Task<PagedResult<Book>> GetAllBooks(int pageNumber, int numberToReturn)
        {
            return await _bookRepo.GetByPage(pageNumber, numberToReturn);
        }
    }
}
