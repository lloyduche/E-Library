using AutoMapper;
using EBookLibrary.DTOs;
using EBookLibrary.DTOs.UserDTOs;
using EBookLibrary.Server.Core.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [Route("delete-user/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            await _userservice.DeleteUser(Id);
            return NoContent();
    }

        [HttpPost]
        [Route("upload-photo/{Id}")]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file, string Id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _userservice.UploadPhoto(file, Id);
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

        [HttpGet]
        [Route("get-user-role")]
        public IActionResult GetUserByRole(string Id)
        {
            var userRole = _userservice.GetUserByRole(Id);

            return Ok(userRole);
        }
    }
}