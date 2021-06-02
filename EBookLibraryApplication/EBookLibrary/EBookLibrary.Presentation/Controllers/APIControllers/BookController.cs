using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class BookController : BaseAPIController
    {
        private readonly IBookServices _bookService;
        private readonly IGenericRepository<Book> _bookRepo;

        public BookController(IBookServices bookService, IGenericRepository<Book> bookRepo)
        {
            _bookService = bookService;
            _bookRepo = bookRepo;
        }

        [HttpPost]
        [Route("add-book")]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            var response = await _bookService.AddBook(addBookDto);
            return Ok(response);
        }

        [HttpPatch]
        [Route("update-book/{Id}")]
        public async Task<IActionResult> UpdateBook(UpdateBookDto updatebookdto, string Id)
        {
            await _bookService.UpdateBook(updatebookdto, Id);
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
            var response = await _bookService.AddReview(addreviewdto);
            return Ok(response);
        }

        [HttpPost]
        [Route("uploadphoto")]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoDto uploadphotodto)
        {
            var response = await _bookService.UploadPhoto(uploadphotodto);
            return Ok(response);
        }

        [HttpPost]
        [Route("search")]
        public IActionResult SearchBooks(SearchParametersDTO1 model)
        {
            if (model.PageNumber == 0) model.PageNumber++;
            if (model.PageSize == 0) model.PageSize = 15;
            if (string.IsNullOrEmpty(model.Query)) return Redirect("/");
            var response = _bookService.Search(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-book-by-id/{Id}")]
        public async Task<IActionResult> GetBook(string Id)
        {
            var response = await _bookService.FindBook(Id);
            return Ok(response);
        }

        [HttpPost]
        [Route("homepagedata")]
        public HomePageDTO GetAllBooksPaginated(HomePageFetchData data)
        {
            return _bookService.GetHomePageData(data);
        }
    }
}
