using Core.DTOs.UserDto;
using Core.Filters;
using Core.Responses;
using Delivery.Controllers;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;

namespace Delivery.Tests.Controller;

public class UserControllerTests
{
    private readonly IUserService _service;
    private readonly UserController _controller;
    public UserControllerTests()
    {
        // Dependencies
        _service = A.Fake<IUserService>();
        
        // SUT
        _controller = new UserController(_service);
    }

    [Fact]
    public async Task UserController_GetAll_ReturnsUsers()
    {
        // Arrange
        var filter = new UserFilter();
        var response = A.Fake<Response<List<GetUserDto>>>();
        A.CallTo(() => _service.GetUsers(filter)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetAll(filter);

        // Assert
        result.Should().BeAssignableTo<Response<List<GetUserDto>>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task UserController_GetUser_ReturnsUser()
    {
        // Arrange
        int id = 1;
        var response = A.Fake<Response<GetUserDto>>();
        A.CallTo(() => _service.GetUserById(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetUser(id);

        // Assert
        result.Should().BeAssignableTo<Response<GetUserDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task UserController_Create_ReturnsUser()
    {
        // Arrange
        var order = A.Fake<CreateUserDto>();
        var response = A.Fake<Response<GetUserDto>>();
        A.CallTo(() => _service.CreateUser(order)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Create(order);

        // Assert
        result.Should().BeAssignableTo<Response<GetUserDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task UserController_Update_ReturnsUser()
    {
        // Arrange
        int id = 1;
        var order = A.Fake<UpdateUserDto>();
        var response = A.Fake<Response<GetUserDto>>();
        A.CallTo(() => _service.UpdateUser(id, order)).Returns(Task.FromResult(response));
        
        // Act
        var result = await _controller.Update(id, order);

        // Assert
        result.Should().BeAssignableTo<Response<GetUserDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task UserController_Delete_ReturnsString()
    {
        // Arrange
        int id = 1;
        var response = new Response<string>("Deleted");
        A.CallTo(() => _service.DeleteUser(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().Be(response);
        result.Should().BeSameAs(response);
    }
}