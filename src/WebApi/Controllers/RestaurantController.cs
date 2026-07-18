using Core.DTOs.RestaurantDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController(IRestaurantService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetRestaurantDto>>> GetAll([FromQuery] RestaurantFilter filter) => await service.GetRestaurants(filter);

    [HttpGet("{id}")]
    public async Task<Response<GetRestaurantDto>> GetRestaurant(int id) => await service.GetRestaurantById(id);

    [HttpPost]
    public async Task<Response<GetRestaurantDto>> Create([FromBody] CreateRestaurantDto dto) => await service.CreateRestaurant(dto);

    [HttpPut("{id}")]
    public async Task<Response<GetRestaurantDto>> Update(int id, [FromBody] UpdateRestaurantDto dto) => await service.UpdateRestaurant(id, dto);

    [HttpDelete("{id}")]
    public async Task<Response<string>> Delete(int id) => await service.DeleteRestaurant(id);
}