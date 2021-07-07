using EBookLibrary.DTOs;
using EBookLibrary.DTOs.BookDTOs;
using EBookLibrary.DTOs.RatingDTOs;
using EBookLibrary.DTOs.ReviewDTOs;
using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.ViewModels;
using EBookLibrary.ViewModels.BookVMs;
using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.RatingsVM;
using EBookLibrary.ViewModels.ReviewVMs;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public class ClientBookService : IClientBookService
    {
        private readonly IAppHttpClient _httpClient;

        public ClientBookService(IServiceProvider serviceProvider)
        {
            _httpClient = serviceProvider.GetRequiredService<IAppHttpClient>();
        }

        public async Task<ExpectedResponse<string>> Add(AddBook model)
        {
            var data = await _httpClient.Create<ExpectedResponse<string>,
               AddBook>("api/v1/Book/add-book", model);

            return data;
        }


        public async Task<bool> DeleteBook(string bookid)
        {
            var data= await _httpClient.Delete($"api/v1/Book/delete/{bookid}");
            return data;
            
        }





        public async Task<BookResponse> UpdateBook(UpdateBookViewModel model, string Id)
        {
            BookResponse response = new BookResponse();

            var data = await _httpClient.Update($"api/v1/Book/update-book/{Id}", model);

            if (data)
            {
                response.Successful = true;
                response.Message = "Updated successfully";
                return response;
            }
            response.Message = "Update Failed";
            return response;
        }

        public async Task<ExpectedResponse<GetBookDetailsResponseVM>> GetBook(string Id)
        {
            var data = await _httpClient.Get<ExpectedResponse<GetBookDetailsResponseVM>>($"api/v1/book/get-book-by-id/{Id}");

            return data;
        }

        public async Task<HomePageViewModel> GetHomePageData(PagingParametersViewModel model)
        {
            return await _httpClient.Create<HomePageViewModel, PagingParametersViewModel>("api/v1/book/homepagedata", model);
        }

        public async Task<bool> AddReview(AddReviewDto model)
        {
            var res = await _httpClient.Create<ExpectedResponse<AddReviewResponseVM>, AddReviewDto>("api/v1/book/add-review", model);

            if (res.Success)
            {
                return true;
            }

            return false; 
        }

        public async Task<bool> AddRating(AddRatingDto model)
        {
            var res = await _httpClient.Create<ExpectedResponse<AddRatingResponseVM>, AddRatingDto>("api/v1/book/add-rating", model);

            if (res.Success)
            {
                return true;
            }

            return false;
        }

        public async Task<PagedResult<BookCardViewModel>> Books(SearchParametersViewModel model)
        {
            var resp = await _httpClient.Create<PagedResult<BookCardViewModel>, SearchParametersViewModel>("api/v1/book/get-books-paginated", model);
            return resp;
        }

        public async Task<bool> UploadPhoto(UploadPhotoVM model)
        {
            var data = await _httpClient.UploadPhoto<ExpectedResponse<string>>(model.BookPhoto, $"http://localhost:44391/api/v1/book/uploadphoto/{model.BookId}");
            if (data)
            {
                return true;
            }

            return false;
            
        }

        public async Task<PagedResult<BookCardViewModel>> Search(SearchParametersViewModel1 model)
        {
            return await _httpClient.Create<PagedResult<BookCardViewModel>, SearchParametersViewModel1>("api/v1/book/search", model);
        }

        public async Task<ExpectedResponse<int>> GetReviewsCount()
        {
            return await _httpClient.Get<ExpectedResponse<int>>($"api/v1/book/get-reviews-count");
        }

        public async Task<ExpectedResponse<int>> GetBooksCount()
        {
            return await _httpClient.Get<ExpectedResponse<int>>($"api/v1/book/get-books-count");
        }
    }
}
