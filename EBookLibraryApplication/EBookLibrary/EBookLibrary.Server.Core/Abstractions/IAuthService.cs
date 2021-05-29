using EBookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EBookLibrary.DTOs.UserDTOs;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IAuthService
    {
        Task<string> Authenticate(string email, string password);

        Task<string> Register(RegisterDTO model);
    }
}
