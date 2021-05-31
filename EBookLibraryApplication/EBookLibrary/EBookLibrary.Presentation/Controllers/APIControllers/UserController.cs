using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Server.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{
    public class UserController : BaseAPIController
    {
        private readonly IUserService _userservice;
        public UserController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateuserdto)
        {
           await _userservice.UpdateUser(updateuserdto);
            return NoContent();
        }


        [HttpDelete]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody] string id)
        {
           await _userservice.DeleteUser(id);
           return NoContent();
        }
    }
}
