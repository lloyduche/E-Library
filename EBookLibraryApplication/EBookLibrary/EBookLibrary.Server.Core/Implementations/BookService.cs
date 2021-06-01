using AutoMapper;
using EBookLibrary.Commons.ExceptionHandler;
using EBookLibrary.Commons.Exceptions;
using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.Commons;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenericRepository<Rating> _ratingRepository;
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;

        public BookService(IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _fileUpload = serviceProvider.GetRequiredService<IFileUpload>();
            _bookRepository = serviceProvider.GetRequiredService<IBookRepository>();
            _ratingRepository = serviceProvider.GetRequiredService<IGenericRepository<Rating>>();
            _reviewRepository = serviceProvider.GetRequiredService<IGenericRepository<Review>>();
            _userManager = userManager;

        }
        public async Task<TResponse<AddBookResponseDto>> AddBook(AddBookDto addbookdto)
        {
            TResponse<AddBookResponseDto> response = new TResponse<AddBookResponseDto>();
            
            //check if dto is valid
            if (addbookdto == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            //create book
            var book = _mapper.Map<AddBookDto,Book>(addbookdto);

            //Add book to database
            var x = await _bookRepository.Insert(book);
            
            //construct response
            var addbookresponsedto = _mapper.Map<AddBookResponseDto>(book);
 
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = addbookresponsedto;
            response.Message = "Book Added Successfully";
            response.Success = true;
            
            return response;

        }

        
        public  async Task<bool> UpdateBook(UpdateBookDto updatebookdto)
        { 
            if (updatebookdto == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            var existingbook = await _bookRepository.Get(updatebookdto.Id);

            //existingbook = _mapper.Map<Book>(updatebookdto);
            _mapper.Map(updatebookdto, existingbook);
            return await _bookRepository.Update(existingbook);

        }

        public async Task<bool> DeleteBook(string bookid)
        {
            var bookToDelete = await _bookRepository.Get(bookid);
            if(bookToDelete == null)
            {
                throw new NotFoundException("Book Does not exist");
            }
            
            return await _bookRepository.Delete(bookToDelete);
        }

        public async Task<TResponse<string>> UploadPhoto(UploadPhotoDto uploadphotodto)
        {
           
            TResponse<string> response = new TResponse<string>();
            UploadAvatarResponse uploadAvatarResponse = new UploadAvatarResponse();
            var file = uploadphotodto.BookPhoto;
            if(file == null)
            {
                throw new BadRequestException("Invalid Photo");
            }

            var book = await _bookRepository.Get(uploadphotodto.BookId);
            if(book == null)
            {
                throw new BadRequestException("Something went wrong");
            }
            
            uploadAvatarResponse = _fileUpload.UploadAvatar(file);
                
            book.AvatarUrl = uploadAvatarResponse.AvatarUrl;
            book.PublicId = uploadAvatarResponse.PublicId;

            await _bookRepository.Update(book);
            
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = uploadAvatarResponse.AvatarUrl;
            response.Message = "Photo Upload Successfull";
            response.Success = true;

            return response;

        }

        public async Task<TResponse<AddRatingResponseDto>> AddRating(AddRatingDto addratingdto)
        {
            var user = _userManager.FindByIdAsync(addratingdto.UserId);
            var book = _bookRepository.Get(addratingdto.BookId);

            if(user == null || book == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            TResponse<AddRatingResponseDto> response = new TResponse<AddRatingResponseDto>();
            var rating = _mapper.Map<AddRatingDto,Rating>(addratingdto);

            var result = await _ratingRepository.Insert(rating);
            if(!result)
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

        public async Task<TResponse<AddReviewResponseDto>> AddReview(AddReviewDto addreviewdto)
        {
            var user = _userManager.FindByIdAsync(addreviewdto.UserId);
            var book = _bookRepository.Get(addreviewdto.BookId);

            if (user == null || book == null)
            {
                throw new BadRequestException("Invalid Input");
            }

            TResponse<AddReviewResponseDto> response = new TResponse<AddReviewResponseDto>();


            var review = _mapper.Map<AddReviewDto,Review>(addreviewdto);

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

        public async Task<TResponse<SearchTermDto>> GetAllBooksWhere(SearchTermDto term)
        {
            TResponse<SearchTermDto> response = new TResponse<SearchTermDto>();
            var book = await _bookRepository.GetAllBooksWhere(term);
            if (book == null)
                throw new NotFoundException("Not available");

            var books = _mapper.Map<SearchTermDto>(book);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "Search Successful";
            response.Success = true;
            response.Data = books;

            return response;
        }

        Task<bool> IBookService.GetAllBooksWhere(SearchTermDto term)
        {
            throw new NotImplementedException();
        }
    }
}
