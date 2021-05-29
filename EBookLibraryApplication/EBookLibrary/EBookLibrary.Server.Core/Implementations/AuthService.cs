using EBookLibrary.DataAccess;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using EBookLibrary.Commons.CustomException;
using EBookLibrary.Commons.ExceptionHandler;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using EBookLibrary.Commons.Exceptions;

namespace EBookLibrary.Server.Core.Implementations
{
    public class AuthService : IAuthService
    {
        private UserManager<User> _userManager;
        private IJWTService _jwtService;
        private IMapper _mapper;


        public AuthService(IServiceProvider serviceProvider)  
        {
            _jwtService = serviceProvider.GetRequiredService<IJWTService>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }


        public async Task<string> Authenticate(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                throw new NotFoundException("User with email provided does not exist");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, password);

            if (!passwordCheck)
            {
                throw new UnauthorizedException("Invalid password");
            }

            var token = await _jwtService.GenerateToken(user);

            return token;
        }

        public async Task<string> Register(RegisterDTO model, string scheme, IUrlHelper url)
        {
           
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if(existingUser != null)
            {
                throw new BadRequestException("User with email already exists");
            }

            var newUser = _mapper.Map<User>(model);

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var urlTobeSentToUserEmail = url.Action("ConfirmEmail", "Account", new { token = emailConfirmationToken, email = newUser.Email }, scheme);

            IdentityResult result = _userManager.CreateAsync(newUser, model.Password).Result;
        }
    }
}
