﻿using EBookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.DTOs.Commons;
using EBookLibrary.DTOs;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IAuthService
    {
        Task<Response<string>> Login(string email, string password);
        Task<Response<string>> Register(RegisterDTO model, string scheme, IUrlHelper url);
        Task<bool> ConfirmEmail(string userid, string token);

        Task<bool> SendResetPasswordLink(string email, IUrlHelper url, string scheme);

        Task<Response<string>> ResetPassword(ResetPasswordDto resetpassword);
    }
}
