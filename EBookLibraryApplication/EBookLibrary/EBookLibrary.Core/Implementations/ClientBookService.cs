﻿using EBookLibrary.Models;
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
    public class ClientBookService: IClientBookService
    {
        private readonly IAppHttpClient _httpClient;
        public ClientBookService(IServiceProvider serviceProvider)
        {
            _httpClient = serviceProvider.GetRequiredService<IAppHttpClient>();
        }

        public async Task<ExpectedResponse<string>> Add(AddBook model)
        {
            var data = await _httpClient.Create<ExpectedResponse<string>,
               AddBook>("api/v1/Auth/addbook", model);

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

        public async Task<UpdateBookViewModel> GetBook(string Id)
        {
            var data = await _httpClient.Get<ExpectedResponse<UpdateBookViewModel> >($"api/v1/book/get-book-by-id/{Id}");
            
            return data.Data;

        }
    }
}
