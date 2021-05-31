using EBookLibrary.Server.Core.Abstractions;
using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.UserVMs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Implementations
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IAppHttpClient _httpClient;
        public AuthenticationService(IServiceProvider serviceProvider)
        {
            _httpClient = serviceProvider.GetRequiredService<IAppHttpClient>();
        }


        public async Task<ExpectedResponse<string>> Register(Users model)
        {

           var data =  await _httpClient.Create<ExpectedResponse<string>,
               Users>("api/v1/Auth/Register", model);
           
            return data;
        }
        public async Task<UpdateResponse> Update(UpdateViewModel model)
        {
            UpdateResponse response = new UpdateResponse();
            var data = await _httpClient.Create<ExpectedResponse<string>,
                UpdateViewModel>("api/v1/Auth/Update", model);
            if (data.Success)
            {
                response.Successful = true;
                response.Message = "You have successfully updated your information";
                return response;
            }
            response.Message = data.Message;
            return response;
        }

    }
}
