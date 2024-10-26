using AutoService.API.Contracts;
using AutoService.Core.Abstractions;
using AutoService.Core.Models;
using AutoService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace AutoService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IService<Users> _service;
        private readonly IUserService _userService;

        public UserController(IService<Users> service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet("usersList")]
        public async Task<ActionResult<List<UsersResponse>>> GetUsers()
        {
            var users = await _service.GetAll();
            var response = users.Select(b => new UsersResponse(b.Id, b.Username, b.Password, b.Email)).ToList();
            return Ok(response);
        }
        [HttpGet("user/{username}")]
        public async Task<ActionResult<UsersResponse>> GetUser(string username)
        {
            var user = await _service.GetByName(username);
            if (user == null)
            {
                return NotFound($"Car with name '{username}' not found.");
            }
            var response = new UsersResponse(user.Id, user.Username, user.Password, user.Email);
            return Ok(response);

        }
        [HttpPost("addUser")]
        public async Task<ActionResult> AddUser([FromBody] UserRequest request)
        {
            var (user, error) = Users.Create(  request.Username,
                                                request.Password,
                                                request.Email);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            await _service.Add(user);

            return Ok();
        }
        [HttpDelete("deleteUser/{username}")]
        public async Task<ActionResult> DeleteUser([FromRoute] string username)
        {
            var user = await _service.GetByName(username);
            if (user == null)
            {
                return NotFound($"User with username '{username}' not found.");
            }

            await _service.Delete(user);
            return Ok($"User '{username}' has been deleted.");
        }
        [HttpPut("updateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserRequest request)
        {
            var user = Users.Create(request.Username, request.Password, request.Email).User;
            await _service.Update(user);

            return Ok($"User '{request.Username}' has been updated.");
        }
        [HttpPost("validateUser")]
        public async Task<ActionResult<UsersResponse>> ValidateUser([FromBody] UserRequest request)
        {
            var user = Users.Create(request.Username, request.Password, request.Email).User;
            user = await _userService.ValidateUser(user);

            var response = new UsersResponse(user.Id, user.Username, user.Password, user.Email);

            return Ok(response);
        }
    }
}
