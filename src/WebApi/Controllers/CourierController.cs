using Core.DTOs.CourierDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourierController(ICourierService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetCourierDto>>> GetAll([FromQuery] CourierFilter filter)
    {
        return await service.GetCouriers(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<GetCourierDto>> GetById(int id)
    {
        return await service.GetCourierById(id);
    }
    
    [HttpPost]
    public async Task<Response<GetCourierDto>> Create([FromBody] CreateCourierDto dto)
    {
        return await service.CreateCourier(dto);
    }

    [HttpPut("{id}")]
    public async Task<Response<GetCourierDto>> Update(int id, [FromBody] UpdateCourierDto dto)
    {
        return await service.UpdateCourier(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteCourier(id);
    }
}