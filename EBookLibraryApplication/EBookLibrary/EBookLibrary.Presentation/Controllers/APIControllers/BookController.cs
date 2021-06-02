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

        [Route("(authors{authorId:int}/books")]
        public async Task<IActionResult> GetBookByAuthor(string authorId)
        {
            var response = await _bookService.GetBookByAuthor(authorId);
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
        
        [HttpGet]
        [Route("get-books-count")]
        public ActionResult GetTotalNumberOfBooks()
        {
            var result = _bookService.GetTotalBooksCount();

            return Ok(result);
        }

        [HttpGet]
        [Route("get-reviews-count")]
        public ActionResult GetTotalNumberOfReviews()
        {
            var result = _bookService.GetTotalReviewsCount();

            return Ok(result);
        }
    }
}
