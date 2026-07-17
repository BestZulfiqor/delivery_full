using Core.DTOs.OrderDetailDto;
using Core.Filters;
using Core.Responses;
using Delivery.Controllers;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;

namespace Delivery.Tests.Controller;

public class OrderDetailControllerTests
{
    private readonly IOrderDetailService _service;
    private readonly OrderDetailController _controller;
    public OrderDetailControllerTests()
    {
        // Dependencies
        _service = A.Fake<IOrderDetailService>();
        
        // SUT
        _controller = new OrderDetailController(_service);
    }

    [Fact]
    public async Task OrderDetailController_GetAll_ReturnsOrderDetails()
    {
        // Arrange
        var filter = new OrderDetailFilter();
        var response = A.Fake<Response<List<GetOrderDetailDto>>>();
        A.CallTo(() => _service.GetOrderDetails(filter)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetAll(filter);

        // Assert
        result.Should().BeAssignableTo<Response<List<GetOrderDetailDto>>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderDetailController_GetOrderDetail_ReturnsOrderDetail()
    {
        // Arrange
        int id = 1;
        var response = A.Fake<Response<GetOrderDetailDto>>();
        A.CallTo(() => _service.GetOrderDetailById(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.GetOrderDetail(id);

        // Assert
        result.Should().BeAssignableTo<Response<GetOrderDetailDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderDetailController_Create_ReturnsOrderDetail()
    {
        // Arrange
        var order = A.Fake<CreateOrderDetailDto>();
        var response = A.Fake<Response<GetOrderDetailDto>>();
        A.CallTo(() => _service.CreateOrderDetail(order)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Create(order);

        // Assert
        result.Should().BeAssignableTo<Response<GetOrderDetailDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderDetailController_Update_ReturnsOrderDetail()
    {
        // Arrange
        int id = 1;
        var order = A.Fake<UpdateOrderDetailDto>();
        var response = A.Fake<Response<GetOrderDetailDto>>();
        A.CallTo(() => _service.UpdateOrderDetail(id, order)).Returns(Task.FromResult(response));
        
        // Act
        var result = await _controller.Update(id, order);

        // Assert
        result.Should().BeAssignableTo<Response<GetOrderDetailDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task OrderDetailController_Delete_ReturnsString()
    {
        // Arrange
        int id = 1;
        var response = new Response<string>("Deleted");
        A.CallTo(() => _service.DeleteOrderDetail(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().Be(response);
        result.Should().BeSameAs(response);
    }
}