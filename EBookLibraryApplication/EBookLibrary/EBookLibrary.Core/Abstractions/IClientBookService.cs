using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
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
        Task<ExpectedResponse<string>> Add(AddBook model);

        Task<BookResponse> UpdateBook(UpdateBookViewModel model, string Id);

        Task<GetBookDetailsResponseVM> GetBook(string Id);

        Task<HomePageViewModel> GetHomePageData(PagingParametersViewModel model);
        Task<bool> AddReview(AddReviewDto model);
        Task<bool> AddRating(AddRatingDto model);
    }
}
