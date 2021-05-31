using EBookLibrary.ViewModels.BookVMs;
using EBookLibrary.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public interface IBookService
    {
        Task<ExpectedResponse<string>> Add(AddBook model);
    }
}
