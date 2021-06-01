using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Mvc;

using System;
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
        [Route("delete-user/{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            await _userservice.DeleteUser(Id);
            return NoContent();
        }

        [HttpPost]
        [Route("upload-photo")]
        public async Task<IActionResult> UploadPhoto([FromForm] PhotoUploadDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _userservice.UploadPhoto(model);
            return Ok(response);
        }
    }
}
