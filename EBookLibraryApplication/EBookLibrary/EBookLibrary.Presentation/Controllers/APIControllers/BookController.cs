﻿using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class BookController : BaseAPIController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {

            _bookService = bookService;
        }

        [HttpPost]
        [Route("add-book")]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            var response = await _bookService.AddBook(addBookDto);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-book")]

        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto updatebookdto)
        {
            await _bookService.UpdateBook(updatebookdto);
            return NoContent();
        }


        [HttpDelete]
        [Route("delete")]

        public async Task<IActionResult> DeleteBook([FromBody] string bookid)
        {
            await _bookService.DeleteBook(bookid);
            return NoContent();
        }


        [HttpPost]
        [Route("add-rating")]
        public async Task<IActionResult> RateBook([FromBody] AddRatingDto addratingdto)
        {
            var response = await _bookService.AddRating(addratingdto);
            return Ok(response);
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> ReviewBook([FromBody] AddReviewDto addreviewdto)
        {
            var response = await  _bookService.AddReview(addreviewdto);
            return Ok(response);
        }

        [HttpPost]
        [Route("uploadphoto")]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoDto uploadphotodto)
        {
            var response = await  _bookService.UploadPhoto(uploadphotodto);
            return Ok(response);
        }

    }
}