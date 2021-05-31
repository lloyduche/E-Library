using EBookLibrary.DTOs;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IUserService
    {
        Task<bool> UpdateUser(UpdateUserDto user);

        Task<bool> DeleteUser(string Id);
    }
}
