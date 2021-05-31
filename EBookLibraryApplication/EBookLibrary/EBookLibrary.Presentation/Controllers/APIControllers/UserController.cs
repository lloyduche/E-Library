using AutoMapper;
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

        [HttpGet]
        [Route("get-user/{Id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var result = await _userservice.GetUserById(Id);

            return Ok(result);
        }
    }
}
