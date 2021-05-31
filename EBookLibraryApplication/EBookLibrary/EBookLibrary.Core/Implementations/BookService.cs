using EBookLibrary.Client.Core.Abstractions;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.ViewModels.BookVMs;
using EBookLibrary.ViewModels.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public class BookService : IBookService
    {
        private readonly IAppHttpClient _httpClient;

        public BookService(IServiceProvider serviceProvider)
        {
            _httpClient = serviceProvider.GetRequiredService<IAppHttpClient>();
        }

        public async Task<BookResponse> UpdateBook(UpdateBookViewModel model)
        {
            BookResponse response = new BookResponse();

            var data = await _httpClient.Update<UpdateBookViewModel>("api/v1/Book/update-book", model);

            if (data)
            {
                response.Successful = true;
                response.Message = "Updated successfully";
                return response;
            }
            response.Message = "Update Failed";
            return response;
        }



    }
}
