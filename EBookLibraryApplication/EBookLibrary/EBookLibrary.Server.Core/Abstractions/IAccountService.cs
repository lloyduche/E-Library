using EBookLibrary.DTOs.Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IAccountService
    {
        Task<bool> ConfirmEmail(string userid, string token);

        Task<bool> SendResetPasswordLink(string email, IUrlHelper url, string scheme);

        Task<IdentityResult> ResetPassword(ResetPasswordDto resetpassword);

    }
}
