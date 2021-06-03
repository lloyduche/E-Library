using AutoMapper;

using EBookLibrary.Commons.ExceptionHandler;
using EBookLibrary.Commons.Exceptions;
using EBookLibrary.Commons.Helpers;
using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.Commons;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class BookService : IBookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Rating> _ratingRepository;
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;
        private readonly IBookRepository _bookRepo;

        public BookService(IServiceProvider serviceProvider, UserManager<User> userManager, IBookRepository bookRepo)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _fileUpload = serviceProvider.GetRequiredService<IFileUpload>();
            _bookRepository = serviceProvider.GetRequiredService<IBookRepository>();
            _categoryRepository = serviceProvider.GetRequiredService<IGenericRepository<Category>>();
            _bookRepo = bookRepo;
            _ratingRepository = serviceProvider.GetRequiredService<IGenericRepository<Rating>>();
            _reviewRepository = serviceProvider.GetRequiredService<IGenericRepository<Review>>();
            _userManager = userManager;
        }

        public async Task<Response<string>> AddBook(AddBookDto addbookdto)
        {
            Response<string> response = new Response<string>();

            //check if dto is valid
            if (addbookdto == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            var findcatid = await _categoryRepository.Find(e => e.Name == addbookdto.CategoryName);

            //create book
            var book = _mapper.Map<AddBookDto, Book>(addbookdto);
            book.CategoryId = findcatid.Id;

            //Add book to database
            await _bookRepository.Insert(book);

            //construct response

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = book.Id;
            response.Message = "Book Added Successfully";
            response.Success = true;

            return response;
        }

        public async Task<bool> UpdateBook(UpdateBookDto updatebookdto, string Id)
        {
            if (updatebookdto == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            var existingbook = await _bookRepository.Get(Id);
            var findcatid = await _categoryRepository.Find(e => e.Name == updatebookdto.Category);

            existingbook.CategoryId = findcatid.Id;
            existingbook.Author = updatebookdto.Author;
            existingbook.Title = updatebookdto.Title;
            existingbook.Pages = updatebookdto.Pages.ToString();
            existingbook.Publisher = updatebookdto.Publisher;
            existingbook.Isbn = updatebookdto.Isbn;
            existingbook.Description = updatebookdto.Description;
            existingbook.CopiesAvailable = updatebookdto.CopiesAvailable;
            existingbook.DatePublished = updatebookdto.DatePublished;

            // _mapper.Map(updatebookdto, existingbook);
            return await _bookRepository.Update(existingbook);
        }

        public async Task<bool> DeleteBook(string bookid)
        {
            var bookToDelete = await _bookRepository.Get(bookid);
            if (bookToDelete == null)
            {
                throw new NotFoundException("Book Does not exist");
            }

            return await _bookRepository.Delete(bookToDelete);
        }

        public async Task<Response<string>> UploadPhoto(IFormFile image, string Id)
        {
            Response<string> response = new Response<string>();
            UploadAvatarResponse uploadAvatarResponse = new UploadAvatarResponse();
            if (image == null)
            {
                throw new BadRequestException("Invalid Photo");
            }

            var book = await _bookRepository.Get(Id);
            if (book == null)
            {
                throw new BadRequestException("Something went wrong");
            }

            uploadAvatarResponse = _fileUpload.UploadAvatar(image);

            book.AvatarUrl = uploadAvatarResponse.AvatarUrl;
            book.PublicId = uploadAvatarResponse.PublicId;

            await _bookRepository.Update(book);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = uploadAvatarResponse.AvatarUrl;
            response.Message = "Photo Upload Successfull";
            response.Success = true;

            return response;
        }

        public async Task<Response<AddRatingResponseDto>> AddRating(AddRatingDto addratingdto)
        {
            var user = _userManager.FindByIdAsync(addratingdto.UserId);
            var book = _bookRepository.Get(addratingdto.BookId);

            if (user == null || book == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            Response<AddRatingResponseDto> response = new Response<AddRatingResponseDto>();
            var rating = _mapper.Map<AddRatingDto, Rating>(addratingdto);

            var result = await _ratingRepository.Insert(rating);
            if (!result)
            {
                throw new BadRequestException("Something went wrong");
            }

            var addRatingResponseDto = _mapper.Map<AddRatingResponseDto>(rating);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = addRatingResponseDto;
            response.Message = "Rating Added";
            response.Success = true;

            return response;
        }

        public async Task<Response<AddReviewResponseDto>> AddReview(AddReviewDto addreviewdto)
        {
            var user = _userManager.FindByIdAsync(addreviewdto.UserId);
            var book = _bookRepository.Get(addreviewdto.BookId);

            if (user == null || book == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            Response<AddReviewResponseDto> response = new Response<AddReviewResponseDto>();

            var review = _mapper.Map<AddReviewDto, Review>(addreviewdto);

            var result = await _reviewRepository.Insert(review);
            if (!result)
            {
                throw new BadRequestException("Something went wrong");
            }
            var addreviewresponsedto = _mapper.Map<AddReviewResponseDto>(review);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = addreviewresponsedto;
            response.Message = "Rating Added";
            response.Success = true;

            return response;
        }

        public async Task<Response<FindBookDto>> FindBook(string Id)
        {
            Response<FindBookDto> response = new Response<FindBookDto>();

            //get reviews and ratings
            var book = await _bookRepo.GetDetailedBook(Id);
            if (book == null)
                throw new NotFoundException("The Book With This Title Cannot Be Found");

            var books = _mapper.Map<FindBookDto>(book);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "Search Successful";
            response.Success = true;
            response.Data = books;

            return response;
        }

        public PagedResult<BookCardDTO> Search(SearchParametersDTO1 model)
        {
            var books = _bookRepo.GetFilteredBooks(model.Query).Paginate(model.PageNumber, model.PageSize);
            var mappedBooks = _mapper.Map<PagedResult<BookCardDTO>>(books);
            return mappedBooks;
        }

        public HomePageDTO GetHomePageData(HomePageFetchData paging)
        {
            var RecentResult = _bookRepo.GetPaginatedBooks().OrderBy(x => x.CreatedAt).Paginate(paging.PageNumberForMostRecent, paging.PageSize);
            var RecentMappedResult = _mapper.Map<PagedResult<BookCardDTO>>(RecentResult);
            var popularResult = _bookRepository.GetPaginatedBooks().OrderBy(x => x.CreatedAt).Paginate(paging.PageNumberForMostPopular, paging.PageSize);
            var PopularMappedResult = _mapper.Map<PagedResult<BookCardDTO>>(popularResult);
            HomePageDTO dto = new HomePageDTO();
            dto.MostPopular = PopularMappedResult;
            dto.Recent = RecentMappedResult;
            dto.PagingParams = paging;
            return dto;
        }

        public PagedResult<BookCardDTO> GetAllBooksPaginated(SearchPagingParametersDTO model)
        {
            var result = _bookRepo.GetPaginatedBooks().OrderBy(x => x.CreatedAt).Paginate(model.PageNumber, model.PageSize);
            return _mapper.Map<PagedResult<BookCardDTO>>(result);
        }

        public Response<int> GetTotalBooksCount()
        {
            Response<int> response = new Response<int>();

            var numOfBooks = _bookRepo.GetTotalNumberOfBooks();

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = numOfBooks;
            response.Success = true;

            return response;
        }


        public Response<int> GetTotalReviewsCount()
        {
            Response<int> response = new Response<int>();

            var numOfReviews = _bookRepo.GetTotalNumberOfReviews();

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = numOfReviews;
            response.Success = true;

            return response;
        }
    }
}
