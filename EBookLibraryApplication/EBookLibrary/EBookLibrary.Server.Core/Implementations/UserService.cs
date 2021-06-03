using AutoMapper;
using EBookLibrary.Commons.ExceptionHandler;
using EBookLibrary.Commons.Exceptions;
using EBookLibrary.Commons.Helpers;
using EBookLibrary.DataAccess.Abstractions;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.Commons;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IFileUpload _fileUpload;

        public UserService(IServiceProvider serviceProvider, IGenericRepository<User> userRepo)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _userRepo = userRepo;
            _fileUpload = serviceProvider.GetRequiredService<IFileUpload>();
        }

        public async Task<bool> DeleteUser(string Id)
        {
            var checkuser = await _userManager.FindByIdAsync(Id);

            if (checkuser == null)
                throw new NotFoundException("User with this Id provided does not exist");

            var result = await _userManager.DeleteAsync(checkuser);

            if (!result.Succeeded)
                throw new BadRequestException("User not Deleted");

            return true;
        }

        public PagedResult<UserDTO> GetAllUsers()
        {
            var users = _userRepo.GetAll().Paginate(1, 5);
            var RecentMappedResult = _mapper.Map<PagedResult<UserDTO>>(users); 
            return RecentMappedResult;
            
        }

        public async Task<bool> UpdateUser(UpdateUserDto updateuserdto)
        {
            var checkuser = await _userManager.FindByIdAsync(updateuserdto.Id);

            if (checkuser == null)
                throw new NotFoundException("User with id provided does not exist");

            checkuser.FirstName = updateuserdto.FirstName;
            checkuser.LastName = updateuserdto.LastName;
            checkuser.Gender = updateuserdto.Gender;
            checkuser.Email = updateuserdto.Email;


            var result = await _userManager.UpdateAsync(checkuser);

            if (!result.Succeeded)
                throw new BadRequestException("User not updated");

            return true;
        }

        public async Task<Response<UserDTO>> GetUserById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            Response<UserDTO> response = new Response<UserDTO>();

            if (user == null)   
            {
                throw new NotFoundException("User does not exist");
            }

            var mappedUser = _mapper.Map<UserDTO>(user);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Success = true;
            response.Data = mappedUser;

            return response;
        }

        public async Task<Response<UserDTO>> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            Response<UserDTO> response = new Response<UserDTO>();

            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }

            var mappedUser = _mapper.Map<UserDTO>(user);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Success = true;
            response.Data = mappedUser;

            return response;
        }
        public async Task<Response<string>> UploadPhoto(IFormFile file, string Id)
        {
            Response<string> response = new Response<string>();
            UploadAvatarResponse uploadAvatarResponse = new UploadAvatarResponse();
            if (file == null)
            {
                throw new BadRequestException("Invalid Photo");
            }

            var user = await _userRepo.Get(Id);
            if (user == null)
            {
                throw new BadRequestException("Something went wrong");
            }

            uploadAvatarResponse = _fileUpload.UploadAvatar(file);

            user.AvatarUrl = uploadAvatarResponse.AvatarUrl;
            user.PublicId = uploadAvatarResponse.PublicId;

            await _userRepo.Update(user);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = uploadAvatarResponse.AvatarUrl;
            response.Message = "Photo Upload Successfull";
            response.Success = true;

            return response;
        }

        public PagedResult<AdminUserDTO> GetAllUser(SearchPagingParametersDTO model)
        {
            var user = _userManager.Users.Paginate(model.PageNumber,model.PageSize);

            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }

            var mappedUser = _mapper.Map<PagedResult<AdminUserDTO>>(user);

           

            return mappedUser;
        }

     

        public Response<int> GetTotalNumberOfUsers()
        {
            Response<int> response = new Response<int>();

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = _userRepo.GetCount();
            response.Success = true;

            return response;
        }

        public async Task<IEnumerable<string>> GetUserByRole(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            
            if(user == null)
            {
                throw new BadRequestException("User doesn't exist");
            }
            var userRole = await _userManager.GetRolesAsync(user);
            return userRole.ToList();
        }
    }
}
