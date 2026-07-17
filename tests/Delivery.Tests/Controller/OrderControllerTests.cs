using Delivery.Controllers;
using FakeItEasy;
using Infrastructure.Interfaces;

namespace Delivery.Tests.Controller;

public class OrderControllerTests
{
    private readonly IOrderService _service;
    private readonly OrderController _orderController;
    public OrderControllerTests()
    {
        // Dependencies
        _service = A.Fake<IOrderService>();
        
        // SUT
        _orderController = new OrderController(_service);
    }

    [Fact]
    public async Task OrderController_GetAll_ReturnsOrders()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
}