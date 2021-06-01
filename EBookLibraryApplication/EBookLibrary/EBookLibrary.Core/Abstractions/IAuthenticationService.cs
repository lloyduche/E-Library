using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.UserVMs;
using System;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Abstractions
{
    public interface IAuthenticationService
    {
        Task<bool> Update(UpdateViewModel model);
        Task<RegistrationResponse> Register(RegisterationViewModel model);

        Task<RegistrationResponse> ForgotPassword(ForgotPasswordViewModel model);

        Task<RegistrationResponse> ResetPassword(PasswordResetViewModel model);

        Task<ExpectedResponse<string>> Login(LoginViewModel model);
    }
}
