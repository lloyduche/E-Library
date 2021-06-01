using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IBookService
    {
        Task<TResponse<AddBookResponseDto>> AddBook(AddBookDto addbookdto);

        Task<bool> UpdateBook(UpdateBookDto updatebookdto);

        Task<bool> DeleteBook(string bookid);

        Task<TResponse<string>> UploadPhoto(UploadPhotoDto uploadphotodto);

        Task<TResponse<AddRatingResponseDto>> AddRating(AddRatingDto addratingdto);

        Task<TResponse<AddReviewResponseDto>> AddReview(AddReviewDto addreviewdto);

        Task<bool> GetAllBooksWhere(SearchTermDto term);

    }
}
