using Core.DTOs.RestaurantDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController(IRestaurantService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetRestaurantDto>>> GetAll([FromQuery] RestaurantFilter filter)
    {
        return await service.GetRestaurants(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<GetRestaurantDto>> GetRestaurant(int id)
    {
        return await service.GetRestaurantById(id);
    }
    
    [HttpPost]
    public async Task<Response<GetRestaurantDto>> Create([FromBody] CreateRestaurantDto dto)
    {
        return await service.CreateRestaurant(dto);
    }

    [HttpPut("{id}")]
    public async Task<Response<GetRestaurantDto>> Update(int id, [FromBody] UpdateRestaurantDto dto)
    {
        return await service.UpdateRestaurant(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteRestaurant(id);
    }
}