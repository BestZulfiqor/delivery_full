using Core.DTOs.MenuDto;
using Core.Filters;
using Core.Responses;
using Delivery.Controllers;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;

namespace Delivery.Tests.Controller;


public class MenuControllerTests
{
    private readonly IMenuService _service;
    private readonly MenuController _controller;

    public MenuControllerTests()
    {
        // Dependencies
        _service = A.Fake<IMenuService>();

        // SUT
        _controller = new MenuController(_service);
    }

    [Fact]
    public async Task MenuController_GetAll_ReturnsMenus()
    {
        // Arrange
        var filter = new MenuFilter();
        var response = A.Fake<Response<List<GetMenuDto>>>();

        A.CallTo(() => _service.GetMenus(filter))
            .Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetAll(filter);

        // Assert
        result.Should().BeAssignableTo<Response<List<GetMenuDto>>>();
    }

    [Fact]
    public async Task MenuController_GetById_ReturnsMenu()
    {
        // Arrange 
        int id = 1;
        var response = A.Fake<Response<GetMenuDto>>();
        
        // Act 
        A.CallTo(() => _service.GetMenuById(id)).Returns(Task.FromResult(response));
        var result = await _controller.GetById(id);
        
        // Assert
        result.Should().BeAssignableTo<Response<GetMenuDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task MenuController_CreateMenu_ReturnsMenu()
    {
        // Arrange  
        var menu = A.Fake<CreateMenuDto>();
        var response = A.Fake<Response<GetMenuDto>>();

        A.CallTo(() => _service.CreateMenu(menu)).Returns(Task.FromResult(response));
        
        // Act
        var result = await _controller.Create(menu);
        
        // Assert 
        result.Should().BeAssignableTo<Response<GetMenuDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task MenuController_Update_ReturnsMenu()
    {
        // Arrange
        int id = 1;
        var menu = A.Fake<UpdateMenuDto>();
        var response = A.Fake<Response<GetMenuDto>>();

        A.CallTo(() => _service.UpdateMenu(id, menu))
            .Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Update(id, menu);

        // Assert
        result.Should().BeAssignableTo<Response<GetMenuDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task MenuController_Delete_ReturnsString()
    {
        // Arrange
        int id = 1;
        var response = new Response<string>("Deleted");
        A.CallTo(() => _service.DeleteMenu(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().BeSameAs(response);
        result.Should().NotBeNull();
    }
}