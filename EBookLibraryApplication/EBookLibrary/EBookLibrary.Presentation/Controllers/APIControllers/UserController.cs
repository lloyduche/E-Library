using AutoMapper;
using EBookLibrary.DTOs;
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

        //[HttpGet("all-users")]
        //public IActionResult GetAllUser()
        //{
        //    var user = _userservice.GetAllUsers();
        //    return Ok(user);
        //}

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
        [HttpPost]
        [Route("get-all-user")]
        public IActionResult GetAllUser(SearchPagingParametersDTO model)
        {
            var result = _userservice.GetAllUser(model);

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
