using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
       // [Authorize(Roles = "Admin")]
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
       [Route("delete/{bookid}")]
        public async Task<IActionResult> DeleteBook(string bookid)
        {
            await _bookService.DeleteBook(bookid);
            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PagedResult<Book>> GetAllBooks(int pageNumber, int numberToReturn)
        {
            return await _bookRepo.GetByPage(pageNumber, numberToReturn);
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
        [Route("uploadphoto/{Id}")]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file, string Id )
        {
            var response = await _bookService.UploadPhoto(file, Id);
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
        [AllowAnonymous]
        public async Task<IActionResult> GetBook(string Id)
        {
            var response = await _bookService.FindBook(Id);
            return Ok(response);
        }

        [HttpPost]
        [Route("homepagedata")]
        [AllowAnonymous]
        public HomePageDTO GetAllBooksPaginated(HomePageFetchData data)
        {
            return _bookService.GetHomePageData(data);
        }

        [HttpPost]
        [Route("get-books-paginated")]
        public ActionResult<PagedResult<BookCardDTO>> GetBooks(SearchPagingParametersDTO model)
        {
            var result = _bookService.GetAllBooksPaginated(model);
            return Ok(result);
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
