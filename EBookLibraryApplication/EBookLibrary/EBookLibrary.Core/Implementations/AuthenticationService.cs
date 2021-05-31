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
               RegisterationViewModel>("api/v1/Auth/Register", model);
            if (data.Success)
            {
                response.Successful = true;
                response.Message = "Registered successfully. Check your email for confirmation link";
                return response;
            }
            response.Message = data.Message;
            return response;
        }

        public async Task<RegistrationResponse> ForgotPassword(ForgotPasswordViewModel model)
        {
            RegistrationResponse response = new RegistrationResponse();

            var data = await _httpClient.Get<ExpectedResponse<string>>($"api/v1/Auth/reset-password-link/{model.Email}");

            if (data.Success)
            {
                response.Successful = true;
                response.Message = "Registered successfully. Check your email for confirmation link";
                return response;
            }
            response.Message = "email not sent";
            return response;
        }


        public async Task<RegistrationResponse> ResetPassword(PasswordResetViewModel model)
        {
            RegistrationResponse response = new RegistrationResponse();

            var data = await _httpClient.Create<ExpectedResponse<string>,
              PasswordResetViewModel>("api/v1/Auth/reset-password", model);

            if (data.Success)
            {
                response.Successful = true;
                response.Message = "Password Changed successfully";
                return response;
            }
            response.Message = "Password Reset Failed";
            return response;
        }
        
    }
}
