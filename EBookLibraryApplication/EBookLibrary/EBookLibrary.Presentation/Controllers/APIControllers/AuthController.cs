using EBookLibrary.DTOs.Commons;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class AuthController : BaseAPIController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _authService.Login(model.Email, model.Password);

            return Ok(user);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var user = await _authService.Register(model, Request.Scheme, Url);

            return Ok(user);
        }

        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmemail)
        {
            await _authService.ConfirmEmail(confirmemail.Email, confirmemail.Token);
            return NoContent();
        }

        [HttpGet]
        [Route("reset-password-link/{email}")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            await _authService.SendResetPasswordLink(email, Url, Request.Scheme);
            return NoContent();
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            return Ok(await _authService.ResetPassword(resetPassword));
        }
    }
}
