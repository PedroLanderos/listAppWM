using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using UserApi.Application.DTOs;
using UserApi.Application.Interfaces;
using UserApi.Application.Mappers;
using UserApi.Application.Responses;

namespace UserApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserInterface userInterface) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateUser(UserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var getEnity = UserMapper.ToEntity(user);
            var response = await userInterface.AddUser(getEnity);

            if (response.Flag) return Ok(response);
            else return BadRequest(ModelState);
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserDTO>> GetUser(int userId)
        {
            var user = await userInterface.GetUserById(userId);
            if(user is null) return NotFound("user not found");

            var (_user, _) = UserMapper.FromEntity(user, null!);

            return _user is not null ? Ok(user) : NotFound("user not found");
        }

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteUser(int userId)
        {
            if (userId < 0) return BadRequest("incorrect id");
            //find user by id
            var user = await GetUser(userId);
            if (user is null) return NotFound("no user found");

            var response = await userInterface.DeleteUser(userId);
            if (response.Flag) return Ok(response);
            else return NotFound("user was not deleted");
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateUser(UserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userToEntity = UserMapper.ToEntity(user);

            var response = await userInterface.UpdateUser(userToEntity);

            if (response.Flag) return Ok(response);
            else return StatusCode(500,"Error while controller updating the user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await userInterface.GetAllUsers();
            if (!users.Any()) return NotFound("No users");

            var (_, usersList) = UserMapper.FromEntity(null, users);
            return usersList!.Any() ? Ok(usersList) : NotFound("no users found");
        }
    }
}
