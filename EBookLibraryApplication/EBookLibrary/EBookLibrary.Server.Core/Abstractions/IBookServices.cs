using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDtos;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IBookServices
    {
        Task<Response<IReadOnlyList<FindBookBySearchDTO>>> GetAllBooksWhere(SearchTermDto term);
        Task<Response<AddBookResponseDto>> AddBook(AddBookDto addbookdto);

        Task<bool> UpdateBook(UpdateBookDto updatebookdto, string Id);

        Task<bool> DeleteBook(string bookid);

        Task<Response<string>> UploadPhoto(UploadPhotoDto uploadphotodto);

        Task<Response<AddRatingResponseDto>> AddRating(AddRatingDto addratingdto);

        Task<Response<AddReviewResponseDto>> AddReview(AddReviewDto addreviewdto);

        Task<Response<FindBookDto>> FindBook(string Id);

        HomePageDTO GetHomePageData(HomePageFetchData paging);
        PagedResult<BookCardDTO> GetAllBooksPaginated(SearchPagingParametersDTO model);

    }
}
