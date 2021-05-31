using EBookLibrary.ViewModels.Common;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.ViewModels.UserVMs
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponse> Register(RegisterationViewModel model);

        Task<RegistrationResponse> ForgotPassword(ForgotPasswordViewModel model);

        Task<RegistrationResponse> ResetPassword(PasswordResetViewModel model);

        Task<ExpectedResponse<string>> Login(LoginViewModel model);
    }
}
