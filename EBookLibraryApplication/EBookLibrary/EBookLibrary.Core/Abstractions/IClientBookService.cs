using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.DTOs;
using EBookLibrary.ViewModels;
using EBookLibrary.ViewModels.BookVMs;
using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.ReviewVMs;
using System;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public interface IClientBookService
    {

        Task<bool> DeleteBook(string Id);

        Task<ExpectedResponse<string>> Add(AddBook model);

        Task<BookResponse> UpdateBook(UpdateBookViewModel model, string Id);

        Task<ExpectedResponse<GetBookDetailsResponseVM>> GetBook(string Id);

        Task<HomePageViewModel> GetHomePageData(PagingParametersViewModel model);

        Task<bool> UploadPhoto(UploadPhotoVM model);
        Task<PagedResult<BookCardViewModel>> Books(SearchParametersViewModel model);

        Task<bool> AddReview(AddReviewDto model);
        Task<bool> AddRating(AddRatingDto model);

        Task<PagedResult<BookCardViewModel>> Search(SearchParametersViewModel1 model);

        Task<ExpectedResponse<int>> GetReviewsCount();

        Task<ExpectedResponse<int>> GetBooksCount();
    }
}
