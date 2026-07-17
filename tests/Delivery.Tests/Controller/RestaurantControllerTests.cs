using Core.DTOs.RestaurantDto;
using Core.Filters;
using Core.Responses;
using Delivery.Controllers;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Interfaces;

namespace Delivery.Tests.Controller;

public class RestaurantControllerTests
{
    private readonly IRestaurantService _service;
    private readonly RestaurantController _controller;
    public RestaurantControllerTests()
    {
        // Dependencies
        _service = A.Fake<IRestaurantService>();
        
        // SUT
        _controller = new RestaurantController(_service);
    }

    [Fact]
    public async Task RestaurantController_GetAll_ReturnsRestaurants()
    {
        // Arrange
        var filter = new RestaurantFilter();
        var response = A.Fake<Response<List<GetRestaurantDto>>>();
        A.CallTo(() => _service.GetRestaurants(filter)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetAll(filter);

        // Assert
        result.Should().BeAssignableTo<Response<List<GetRestaurantDto>>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task RestaurantController_GetRestaurant_ReturnsRestaurant()
    {
        // Arrange
        int id = 1;
        var response = A.Fake<Response<GetRestaurantDto>>();
        A.CallTo(() => _service.GetRestaurantById(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetRestaurant(id);

        // Assert
        result.Should().BeAssignableTo<Response<GetRestaurantDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task RestaurantController_Create_ReturnsRestaurant()
    {
        // Arrange
        var order = A.Fake<CreateRestaurantDto>();
        var response = A.Fake<Response<GetRestaurantDto>>();
        A.CallTo(() => _service.CreateRestaurant(order)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Create(order);

        // Assert
        result.Should().BeAssignableTo<Response<GetRestaurantDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task RestaurantController_Update_ReturnsRestaurant()
    {
        // Arrange
        int id = 1;
        var order = A.Fake<UpdateRestaurantDto>();
        var response = A.Fake<Response<GetRestaurantDto>>();
        A.CallTo(() => _service.UpdateRestaurant(id, order)).Returns(Task.FromResult(response));
        
        // Act
        var result = await _controller.Update(id, order);

        // Assert
        result.Should().BeAssignableTo<Response<GetRestaurantDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task RestaurantController_Delete_ReturnsString()
    {
        // Arrange
        int id = 1;
        var response = new Response<string>("Deleted");
        A.CallTo(() => _service.DeleteRestaurant(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().Be(response);
        result.Should().BeSameAs(response);
    }
}