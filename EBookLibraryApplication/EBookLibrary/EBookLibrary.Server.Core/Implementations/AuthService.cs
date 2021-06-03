using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

using EBookLibrary.Commons.CustomException;
using EBookLibrary.Commons.ExceptionHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using EBookLibrary.Commons.Exceptions;
using AutoMapper;
using EBookLibrary.DTOs.Commons;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using EBookLibrary.DTOs;
using System.Net;

namespace EBookLibrary.Server.Core.Implementations
{
    public class AuthService : IAuthService
    {
        private UserManager<User> _userManager;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly SignInManager<User> _signInManager;


        public AuthService(IServiceProvider serviceProvider, SignInManager<User> signInManager)  
        {
            _jwtService = serviceProvider.GetRequiredService<IJWTService>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _mailService = serviceProvider.GetRequiredService<IMailService>();
            _signInManager = signInManager;

        }


        public async Task<Response<LoginResponseDto>> Login(string email, string password)
        {
            Response<LoginResponseDto> responseObject = new Response<LoginResponseDto>();

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException("User with email provided does not exist");
            }

           
            //var passwordCheck = await _userManager.CheckPasswordAsync(user, password);
            var passwordCheck = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (!passwordCheck.Succeeded)
            {
                throw new BadRequestException("Username or password is not correct");
            }

            var token = await _jwtService.GenerateToken(user);

            LoginResponseDto loginresponse = new LoginResponseDto 
            {
                Token = token,
                Role = string.Join(" ,", await _userManager.GetRolesAsync(user)),
                UserId = user.Id
            };


            responseObject.StatusCode = (int) HttpStatusCode.OK;
            responseObject.Data = loginresponse;
            responseObject.Message = "Login successful";
            responseObject.Success = true;

            return responseObject;
        }

        public async Task<Response<string>> Register(RegisterDTO model, string scheme, IUrlHelper url)
        {

            Response<string> responseObject = new Response<string>();
           
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if(existingUser != null)
            {
                throw new BadRequestException("User with email already exists");
            }

            var newUser = _mapper.Map<RegisterDTO, User>(model);

            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException("User not created");
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var encodedToken = Encoding.UTF8.GetBytes(emailConfirmationToken);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var urlTobeSentToUserEmail = url.Action("ConfirmEmail", "Account", new { token = validToken, email = newUser.Email }, scheme);

            var name = $"{newUser.FirstName} {newUser.LastName}";

            var response = await SendMailAsync(name, "Confirm Email", urlTobeSentToUserEmail ,"Click the link below to confirm your email",model.Email);

            if (!response)
            {
                var deleteUser = await _userManager.DeleteAsync(newUser);
                throw new BadRequestException("Email not confirmed");
            }

            responseObject.StatusCode = (int)HttpStatusCode.Created;
            responseObject.Message = "Registration successful";
            responseObject.Success = true;
            return responseObject;
        }

        public async Task<bool> ConfirmEmail(string useremail, string token)
        {
            User user = await _userManager.FindByEmailAsync(useremail);
            if (user == null)
            {
                throw new AccessViolationException("Access Denied");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
    
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Something went wrong");
            }

            return true;
        }

        public async Task<bool> SendResetPasswordLink(string email, IUrlHelper url, string scheme)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new BadRequestException("Your Email is not yet Verified");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var resetLink = url.Action("ResetPassword", "Account", new { token = validToken, email = user.Email }, scheme);

            var name = $"{user.FirstName} {user.LastName}";

            var response = await SendMailAsync(name, "Password Reset", resetLink, "Click the link below to rest your password, link expires in the next 10mins", email);

            if (!response)
            {
                throw new BadRequestException("Could not Send mail");
            }
            return true;
        }


        public async Task<Response<string>> ResetPassword(ResetPasswordDto resetpassword)
        {
            Response<string> responseObject = new Response<string>();
            var user = await _userManager.FindByEmailAsync(resetpassword.Email);
            if (user == null)
            {
                throw new BadRequestException("User does not exist");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(resetpassword.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ResetPasswordAsync(user, normalToken, resetpassword.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Invalid credentials");
            }
            responseObject.StatusCode = (int)HttpStatusCode.OK;
            responseObject.Message = "Password Reset Successful";
            responseObject.Success = true;
            return responseObject;
        }

        private async Task<bool> SendMailAsync(string name, string subject, string link, string body, string recipientmail)
        {
            var mailrequest = new MailRequest
            {
                Name = name,
                Subject = subject,
                Body = body,
                Link = link,
                RecipientMail=recipientmail

            };
            await _mailService.SendEmailAsync(mailrequest);
            return true;
        }

    }
}
