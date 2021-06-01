using EBookLibrary.ViewModels.BookVMs;
using EBookLibrary.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public interface IClientBookService
    {
        Task<ExpectedResponse<string>> Add(AddBook model);

        Task<BookResponse> UpdateBook(UpdateBookViewModel model, string Id);

        Task<UpdateBookViewModel> GetBook(string Id);
    }
}
