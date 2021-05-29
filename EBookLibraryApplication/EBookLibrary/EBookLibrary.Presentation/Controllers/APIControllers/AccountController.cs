using EBookLibrary.DTOs.Commons;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class AccountController : BaseAPIController
    {
        private readonly IAccountService _accountService;
        
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmemail)
        {
            await _accountService.ConfirmEmail(confirmemail.Email, confirmemail.Token);
            return NoContent();

        }

       
        [HttpPost]
        [Route("reset-password-link")]
        public async Task<IActionResult> SendResetPasswordLink(GetEmailToResetPasswordDto model)
        {
            var response = await _accountService.SendResetPasswordLink(model.Email,Url, Request.Scheme);
            if (response)
            {
                return Ok();
            }

            return NotFound();

            
        }


        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            
            return Ok(await _accountService.ResetPassword(resetPassword));
        }



    }
}
