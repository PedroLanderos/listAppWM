using UserApi.Application.Interfaces;
using UserApi.Presentation.Controllers;
using FakeItEasy;
using UserApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using UserApi.Application.DTOs;
using UserApi.Application.Responses;

namespace UnitTest.UserApi.Controllers
{
    public class UserControllerTest
    {
        private readonly UserInterface userInterface;
        private readonly UsersController usersController;

        public UserControllerTest()
        {
            userInterface = A.Fake<UserInterface>();
            usersController = new UsersController(userInterface);
        }

        [Fact]
        public async Task GetUsersOk()
        {
            var users = new List<UserEntity>()
            {
                new(){Id = 1, Name = "User 1", EmailAddress = "test1@gmail.com", Password = "1234"},
                new(){Id = 2, Name = "User 2", EmailAddress = "test2@gmail.com", Password = "1234"}
            };

            A.CallTo(() => userInterface.GetAllUsers()).Returns(users);
            
            var result = await usersController.GetUsers();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);

            var returnedUsers = okResult.Value as IEnumerable<UserDTO>;
            returnedUsers.Should().NotBeNull();
            returnedUsers.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUsersNotFound()
        {
            A.CallTo(()=> userInterface.GetAllUsers()).Returns(new List<UserEntity>());

            var result = await usersController.GetUsers();

            var notFoundResult = result.Result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task AddUserOk()
        {
            var userDto = new UserDTO(1, "pedro", "hola@gmail.com", "1234");
            var response = new ApiResponse(true, "user created");

            A.CallTo(() => userInterface.AddUser(A<UserEntity>.Ignored)).Returns(response);
            var result = await usersController.CreateUser(userDto);

            var okResult = result.Result as OkObjectResult;
            okResult!.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task AddUserBadRequest()
        {
            var userDto = new UserDTO(1, "pedro", "hola@gmail.com", "1234");
            var response = new ApiResponse(false, "user was not created");

            A.CallTo(() => userInterface.AddUser(A<UserEntity>.Ignored)).Returns(response);
            var result = await usersController.CreateUser(userDto);

            var okResult = result.Result as BadRequestObjectResult;
            okResult!.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetUserByIdOk()
        {

        }

        public async Task GetUserByIdBadRequest()
        {

        }
    }
}