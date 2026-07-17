using Core.DTOs.CourierDto;
using Core.Filters;
using Core.Responses;
using Delivery.Controllers;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Interfaces;

namespace Delivery.Tests.Controller;

public class CourierControllerTests
{
    private readonly ICourierService _service;
    private readonly CourierController _courierController;

    public CourierControllerTests()
    {
        // Dependencies
        _service = A.Fake<ICourierService>();

        // SUT
        _courierController = new CourierController(_service);
    }

    [Fact]
    public async Task CourierController_GetAll_ReturnsCouriers()
    {
        // Arrange
        var filter = new CourierFilter();
        var response = A.Fake<Response<List<GetCourierDto>>>();

        A.CallTo(() => _service.GetCouriers(filter))
            .Returns(Task.FromResult(response));

        // Act
        var result = await _courierController.GetAll(filter);

        // Assert
        result.Should().BeAssignableTo<Response<List<GetCourierDto>>>();
    }

    [Fact]
    public async Task CourierController_GetById_ReturnsCourier()
    {
        // Arrange 
        int id = 1;
        var response = A.Fake<Response<GetCourierDto>>();
        
        // Act 
        A.CallTo(() => _service.GetCourierById(id)).Returns(Task.FromResult(response));
        var result = await _courierController.GetById(id);
        
        // Assert
        result.Should().BeAssignableTo<Response<GetCourierDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task CourierController_CreateCourier_ReturnsCourier()
    {
        // Arrange  
        var courier = A.Fake<CreateCourierDto>();
        var response = A.Fake<Response<GetCourierDto>>();

        A.CallTo(() => _service.CreateCourier(courier)).Returns(Task.FromResult(response));
        
        // Act
        var result = await _courierController.Create(courier);
        
        // Assert 
        result.Should().BeAssignableTo<Response<GetCourierDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task CourierController_Update_ReturnsCourier()
    {
        // Arrange
        int id = 1;
        var courier = A.Fake<UpdateCourierDto>();
        var response = A.Fake<Response<GetCourierDto>>();

        A.CallTo(() => _service.UpdateCourier(id, courier))
            .Returns(Task.FromResult(response));

        // Act
        var result = await _courierController.Update(id, courier);

        // Assert
        result.Should().BeAssignableTo<Response<GetCourierDto>>();
        result.Should().BeSameAs(response);
    }

    [Fact]
    public async Task CourierController_Delete_ReturnsString()
    {
        // Arrange
        int id = 1;
        var response = new Response<string>("Delete courier");
        A.CallTo(() => _service.DeleteCourier(id)).Returns(Task.FromResult(response));

        // Act
        var result = await _courierController.Delete(id);

        // Assert
        result.Should().BeSameAs(response);
        result.Should().NotBeNull();
    }
}