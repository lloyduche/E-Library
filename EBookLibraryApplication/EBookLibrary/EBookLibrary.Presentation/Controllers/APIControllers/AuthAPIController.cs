using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Server.Core.Abstractions;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class AuthAPIController : BaseAPIController
    {
        private readonly IAuthService _authService;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }

       [HttpPost("Login")]
       [AllowAnonymous]
       public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _authService.Authenticate(model.Email, model.Password);

            return Ok(user);
        }

        [HttpPost("LRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var user = await _authService.Register(model);

            return Ok(user);
        }
    }
}
