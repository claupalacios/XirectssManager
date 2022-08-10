using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserMS.Dtos;
using UserMS.Models;
using UserMS.Services.Interfaces;

namespace UserMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("active-users")]
        [ProducesResponseType(typeof(Response<List<User>>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllActiveUsers()
        {
            var response = _userService.GetAllActiveUsers();

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<object>), 200)]
        [ProducesResponseType(400)]
        public IActionResult AddUser(UserDto user)
        {
            var response = _userService.AddUser(user);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(Response<object>), 200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateUserState(int userId, bool active)
        {
            var response = _userService.UpdateUserState(userId, active);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
