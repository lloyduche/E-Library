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


        public async Task<RegistrationResponse> Register(RegisterationViewModel model)
        {
            RegistrationResponse response = new RegistrationResponse();

           var data =  await _httpClient.Create<ExpectedResponse<string>,
               RegisterationViewModel>("api/v1/Authentication/Register", model);
            if (data.Success)
            {
                response.Successful = true;
                response.Message = "Registered successfully. Check your email for confirmation link";
                return response;
            }
            response.Message = data.Message;
            return response;
        }

    }
}
