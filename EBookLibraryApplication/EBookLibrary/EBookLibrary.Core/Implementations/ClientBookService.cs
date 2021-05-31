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
    }
}
