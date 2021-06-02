using EBookLibrary.ViewModels;
using EBookLibrary.ViewModels.BookVMs;
using EBookLibrary.ViewModels.Common;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public interface IClientBookService
    {
        Task<ExpectedResponse<string>> Add(AddBook model);

        Task<BookResponse> UpdateBook(UpdateBookViewModel model, string Id);

        Task<UpdateBookViewModel> GetBook(string Id);

        Task<HomePageViewModel> GetHomePageData(PagingParametersViewModel model);

        Task<bool> UploadPhoto(UploadPhotoVM model);
    }
}
