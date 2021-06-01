using EBookLibrary.DTOs;
using EBookLibrary.DTOs.UserDTOs;

using System;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Abstractions
{
    public interface IUserService
    {
        Task<bool> UpdateUser(UpdateUserDto user);

        Task<bool> DeleteUser(string Id);

        Task<Response<UserDTO>> GetUserById(string Id);

        Task<Response<UserDTO>> GetUserByEmail(string Id);

        Task<Response<string>> UploadPhoto(PhotoUploadDTO photoUploadDTO);
    }
}
