using AutoMapper;
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

        private readonly IMapper _mapper;
        public UserController(IUserService userservice, IMapper mapper)
        {
            _userservice = userservice;
            _mapper = mapper;
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

        [HttpPost]
        [Route("upload-photo")]
        public async Task<IActionResult> UploadPhoto([FromForm] PhotoUploadDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _userservice.UploadPhoto(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-user/{Id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var result = await _userservice.GetUserById(Id);

            return Ok(result);
        }

        [HttpGet]
        [Route("get-users-count")]
        public ActionResult GetTotalNumberOfUsers()
        {
            var result = _userservice.GetTotalNumberOfUsers();

            return Ok(result);
        }
    }
}
