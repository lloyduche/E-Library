using EBookLibrary.Commons.Exceptions;
using EBookLibrary.DTOs.Commons;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{


    public class AccountService : IAccountService
    {
        

        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;

        public AccountService(UserManager<User> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
            
        }
        public async Task<bool> ConfirmEmail(string useremail,string token)
        {
            User user = await _userManager.FindByEmailAsync(useremail);
            if(user == null)
            {
                throw new AccessViolationException("Access Denied");
            }
            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.EmailConfirmationTokenProvider, "ConfirmEmail", token);
            if (!isValidToken)
            {
                throw new BadRequestException("Token Expired");
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            
            if (!result.Succeeded)
            {
                throw new BadRequestException("Something went wrong");
            }

            return true;
        }

        public async Task<bool> SendResetPasswordLink(string email, IUrlHelper url, string scheme)
        {
            var user = await  _userManager.FindByEmailAsync(email);

            if(user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new BadRequestException("Your Email is not yet Verified");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = url.Action("ResetPassword", "Account", new {token = token, email = user.Email }, scheme);

            var name = $"{user.FirstName} {user.LastName}";
           
            var response = await SendMailAsync(name,"Password Reset", "Click the link below to rest your password, link expires in the next 10mins",resetLink);
            
            if (!response)
            {
                throw new BadRequestException("Could not Send mail");
            }
            return true;
        }


        public async Task<IdentityResult> ResetPassword(ResetPasswordDto resetpassword)
        {
            var user = await _userManager.FindByEmailAsync(resetpassword.Email);
            if(user == null)
            {
                throw new BadRequestException("User does not exist");
            }

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.EmailConfirmationTokenProvider, "ResetPassword", resetpassword.Token);
            if (!isValidToken)
            {
                throw new BadRequestException("Token is invalid");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetpassword.Token, resetpassword.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Something went wrong");
            }
            return result;
        }

        private async Task<bool> SendMailAsync( string name, string subject, string link, string body)
        {
            var mailrequest = new MailRequest
            {
                Name = name,
                Subject = subject,
                Body = body,
                Link = link

            };
            await _mailService.SendEmailAsync(mailrequest);
            return true;
        }

        
    }
}
