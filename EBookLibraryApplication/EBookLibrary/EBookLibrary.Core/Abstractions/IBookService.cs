using EBookLibrary.ViewModels.BookVMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Abstractions
{
    public interface IBookService
    {
        Task<BookResponse> UpdateBook(UpdateBookViewModel model);
    }
}
