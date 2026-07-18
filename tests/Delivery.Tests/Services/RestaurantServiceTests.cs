using System.Net;
using AutoMapper;
using Core.DTOs.RestaurantDto;
using Core.Entities;
using Core.Filters;
using Core.Responses;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Services;

namespace Delivery.Tests.Services;

public class RestaurantServiceTests
{
    private readonly IRestaurantRepository _repository;
    private readonly IMapper _mapper;
    private readonly RestaurantService _service;

    public RestaurantServiceTests()
    {
        // Dependencies - Инициализируем фейк репозитори и маппер
        _repository = A.Fake<IRestaurantRepository>();
        _mapper = A.Fake<IMapper>();

        // SUT - Создаём реальный сервис и внедряем наши фейки
        _service = new RestaurantService(_repository, _mapper);
    }

    [Fact]
    public async Task RestauranService_GetById_ReturnsRestaurant()
    {
        // Arrange
        int nonExistingId = 99;
        int existId = 1;
        var restaurant = A.Fake<Restaurant>();
        A.CallTo(() => _repository.GetByIdAsync(nonExistingId))!.Returns(Task.FromResult<Restaurant>(null));
        A.CallTo(() => _repository.GetByIdAsync(existId))!.Returns(Task.FromResult(restaurant));

        // Act
        var resultNull = await _service.GetRestaurantById(nonExistingId);
        var resultNotNull = await _service.GetRestaurantById(existId);

        // Assert
        resultNull.IsSucced.Should().BeFalse();
        resultNotNull.IsSucced.Should().BeTrue();
    }

    [Fact]
    public async Task GetRestaurantById_WhenRepositoryThrowsException_ReturnsFailureOrThrows()
    {
        // Arrange
        int id = 1;
        var expectedException = new Exception("Database is temporary not available");

        A.CallTo(() => _repository.GetByIdAsync(id))
            .Throws(expectedException);

        // Act & Assert
        Func<Task> act = async () => await _service.GetRestaurantById(id);
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Database is temporary not available");
    }

    [Fact]
    public async Task RestaurantService_GetRestaurants_ReturnsRestaurants()
    {
        // Arrange
        var filter = new RestaurantFilter();
        var restaurants = new List<Restaurant>()
        {
            new() { Id = 1, Name = "Burger" },
            new() { Id = 2, Name = "Chicken" },
        };

        var dtos = new List<GetRestaurantDto>()
        {
            new() { Id = 1, Name = "Burger" },
            new() { Id = 2, Name = "Chicken" },
        };

        A.CallTo(() => _repository.GetPagedRestaurantsAsync(filter)).Returns((restaurants, restaurants.Count));
        A.CallTo(() => _mapper.Map<List<GetRestaurantDto>>(restaurants)).Returns(dtos);

        // Act
        var result = await _service.GetRestaurants(filter);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeSameAs(dtos);
        result.Should().BeAssignableTo(typeof(PagedResponse<List<GetRestaurantDto>>));
        result.IsSucced.Should().BeTrue();
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public async Task RestaurantService_CreateRestaurant_ReturnsRestaurant()
    {
        // Arrange
        var dto = new CreateRestaurantDto()
        {
            Name = "Burger", Address = "Dushanbe", ContactPhone = "205221144",
            DeliveryPrice = 10, Description = "Zuri bad", IsActive = true, MinOrderAmount = 1, Rating = 4,
            WorkingHours = "from 09:00 to 18:00"
        };
        var restaurantWithoutId = new Restaurant()
        {
            Name = "Burger", Address = "Dushanbe", ContactPhone = "205221144",
            DeliveryPrice = 10, Description = "Zuri bad", IsActive = true, MinOrderAmount = 1, Rating = 4,
            WorkingHours = "from 09:00 to 18:00"
        };
        var expectedResultDto = new GetRestaurantDto()
        {
            Id = 1, Name = "Burger", Address = "Dushanbe", ContactPhone = "205221144",
            DeliveryPrice = 10, Description = "Zuri bad", IsActive = true, MinOrderAmount = 1, Rating = 4,
            WorkingHours = "from 09:00 to 18:00"
        };

        A.CallTo(() => _mapper.Map<Restaurant>(dto))
            .Returns(restaurantWithoutId);

        A.CallTo(() => _repository.AddAsync(restaurantWithoutId))
            .Returns(Task.CompletedTask); // если AddAsync возвращает Task

        A.CallTo(() => _repository.SaveChangesAsync())
            .Returns(1);

        A.CallTo(() => _mapper.Map<GetRestaurantDto>(restaurantWithoutId))
            .Returns(expectedResultDto);

        // Act
        var result = await _service.CreateRestaurant(dto);

        // Assert
        result.IsSucced.Should().BeTrue();
    }

    [Fact]
    public async Task RestaurantService_UpdateRestaurant_ReturnsRestaurant()
    {
        int id = 1;
        var dto = new UpdateRestaurantDto()
        {
            Name = "Burger", Address = "Dushanbe", ContactPhone = "205221144",
            DeliveryPrice = 10, Description = "Zuri bad", IsActive = true, MinOrderAmount = 1, Rating = 4,
            WorkingHours = "from 09:00 to 18:00"
        };

        var restaurant = new Restaurant()
        {
            Id = 1, Name = "Burger", Address = "Dushanbe", ContactPhone = "205221144",
            DeliveryPrice = 10, Description = "Zuri bad", IsActive = true, MinOrderAmount = 1, Rating = 4,
            WorkingHours = "from 09:00 to 18:00"
        };

        var expectedResultDto = new GetRestaurantDto()
        {
            Id = 1, Name = "Burger", Address = "Dushanbe", ContactPhone = "205221144",
            DeliveryPrice = 10, Description = "Zuri bad", IsActive = true, MinOrderAmount = 1, Rating = 4,
            WorkingHours = "from 09:00 to 18:00"
        };

        A.CallTo(() => _repository.GetByIdAsync(id)).Returns(restaurant);
        A.CallTo(() => _mapper.Map(dto, restaurant)).Returns(restaurant);
        A.CallTo(() => _repository.Update(restaurant));
        A.CallTo(() => _repository.SaveChangesAsync()).Returns(1);
        A.CallTo(() => _mapper.Map<GetRestaurantDto>(restaurant)).Returns(expectedResultDto);

        // Act
        var result = await _service.UpdateRestaurant(id, dto);

        // Assert
        result.IsSucced.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Name.Should().Be("Burger");
    }

    [Fact]
    public async Task RestaurantService_DeleteRestaurant_ReturnsDeleted()
    {
        // Arrange
        int id = 1;
        var restaurant = new Restaurant()
        {
            Id = 1, Name = "Burger", Address = "Dushanbe", ContactPhone = "205221144",
            DeliveryPrice = 10, Description = "Zuri bad", IsActive = true, MinOrderAmount = 1, Rating = 4,
            WorkingHours = "from 09:00 to 18:00"
        };
        
        A.CallTo(() => _repository.GetByIdAsync(id)).Returns(restaurant);
        A.CallTo(() => _repository.Delete(restaurant));
        A.CallTo(() => _repository.SaveChangesAsync()).Returns(0);

        // Act
        var result = await _service.DeleteRestaurant(id);

        // Assert
        result.IsSucced.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().Be("Deleted");
    }
}