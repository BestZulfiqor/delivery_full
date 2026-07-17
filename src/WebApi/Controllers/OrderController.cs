using Core.DTOs.OrderDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetOrderDto>>> GetAll([FromQuery] OrderFilter filter)
    {
        return await service.GetOrders(filter);
    }

    [HttpGet("{id:int}")]
    public async Task<Response<GetOrderDto>> GetOrder(int id)
    {
        return await service.GetOrderById(id);
    }

    [HttpPut]
    public async Task<Response<GetOrderDto>> Update(int id, UpdateOrderDto dto)
    {
        return await service.UpdateOrder(id, dto);
    }

    [HttpPost]
    public async Task<Response<GetOrderDto>> Create(CreateOrderDto dto)
    {
        return await service.CreateOrder(dto);
    }

    [HttpDelete]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteOrder(id);
    }
}