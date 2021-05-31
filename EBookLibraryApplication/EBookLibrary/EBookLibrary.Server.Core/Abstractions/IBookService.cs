using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IBookService
    {
        Task<Response<AddBookResponseDto>> AddBook(AddBookDto addbookdto);

        Task<bool> UpdateBook(UpdateBookDto updatebookdto);

        Task<bool> DeleteBook(string bookid);

        Task<Response<string>> UploadPhoto(UploadPhotoDto uploadphotodto);

        Task<Response<AddRatingResponseDto>> AddRating(AddRatingDto addratingdto);

        Task<Response<AddReviewResponseDto>> AddReview(AddReviewDto addreviewdto);


    }
}
