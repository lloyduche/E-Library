using AutoMapper;
using EBookLibrary.Commons.ExceptionHandler;
using EBookLibrary.Commons.Exceptions;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Models;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EBookLibrary.Server.Core.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

        }

        public async Task<bool> DeleteUser(string Id)
        {
            var checkuser = await _userManager.FindByIdAsync(Id);

            if(checkuser == null)
                throw new NotFoundException("User with this Id provided does not exist");

            var result = await _userManager.DeleteAsync(checkuser);
           
           if (!result.Succeeded)
                throw new BadRequestException("User not Deleted");

            return true;
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
            
                
                //_mapper.Map<UpdateUserDto, User>(updateuserdto);

            var result = await _userManager.UpdateAsync(checkuser);

            if(!result.Succeeded)
                throw new BadRequestException("User not updated");

            return true;
        }
    }
}
