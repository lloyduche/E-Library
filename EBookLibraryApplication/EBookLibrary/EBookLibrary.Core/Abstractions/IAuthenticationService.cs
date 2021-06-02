using EBookLibrary.ViewModels.Common;
using EBookLibrary.ViewModels.UserVMs;
using System;
using System.Threading.Tasks;

namespace EBookLibrary.Client.Core.Abstractions
{
    public interface IAuthenticationService
    {
        Task<bool> UpdateUser(UpdateViewModel model);
        Task<RegistrationResponse> Register(RegisterationViewModel model);

        Task<RegistrationResponse> ForgotPassword(ForgotPasswordViewModel model);

        Task<RegistrationResponse> ResetPassword(PasswordResetViewModel model);

        Task<ExpectedResponse<LoginResponseVM>> Login(LoginViewModel model);

        Task<bool> DeleteUser(string Id);
    }
}
