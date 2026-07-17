using Core.DTOs.OrderDto;
using Core.Filters;
using Core.Responses;
using Delivery.Controllers;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;

namespace Delivery.Tests.Controller;

public class OrderControllerTests
{
    private readonly IOrderService _service;
    private readonly OrderController _controller;
    public OrderControllerTests()
    {
        // Dependencies
        _service = A.Fake<IOrderService>();
        
        // SUT
        _controller = new OrderController(_service);
    }

    [Fact]
    public async Task OrderController_GetAll_ReturnsOrders()
    {
        // Arrange
        var filter = new OrderFilter();
        var response = A.Fake<Response<List<GetOrderDto>>>();
        A.CallTo(() => _service.GetOrders(filter)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetAll(filter);

        // Assert
        result.Should().BeAssignableTo<Response<List<GetOrderDto>>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderController_GetOrder_ReturnsOrder()
    {
        // Arrange
        int id = 1;
        var response = A.Fake<Response<GetOrderDto>>();
        A.CallTo(() => _service.GetOrderById(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetOrder(id);

        // Assert
        result.Should().BeAssignableTo<Response<GetOrderDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderController_Create_ReturnsOrder()
    {
        // Arrange
        var order = A.Fake<CreateOrderDto>();
        var response = A.Fake<Response<GetOrderDto>>();
        A.CallTo(() => _service.CreateOrder(order)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Create(order);

        // Assert
        result.Should().BeAssignableTo<Response<GetOrderDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderController_Update_ReturnsOrder()
    {
        // Arrange
        int id = 1;
        var order = A.Fake<UpdateOrderDto>();
        var response = A.Fake<Response<GetOrderDto>>();
        A.CallTo(() => _service.UpdateOrder(id, order)).Returns(Task.FromResult(response));
        
        // Act
        var result = await _controller.Update(id, order);

        // Assert
        result.Should().BeAssignableTo<Response<GetOrderDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderController_Delete_ReturnsString()
    {
        // Arrange
        int id = 1;
        var response = new Response<string>("Deleted");
        A.CallTo(() => _service.DeleteOrder(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().Be(response);
        result.Should().BeSameAs(response);
    }
}