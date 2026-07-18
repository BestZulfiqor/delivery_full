using Core.DTOs.OrderDetailDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailController(IOrderDetailService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetOrderDetailDto>>> GetAll([FromQuery] OrderDetailFilter filter) => await service.GetOrderDetails(filter);

    [HttpGet("{id}")]
    public async Task<Response<GetOrderDetailDto>> GetOrderDetail(int id) => await service.GetOrderDetailById(id);

    [HttpPost]
    public async Task<Response<GetOrderDetailDto>> Create([FromBody] CreateOrderDetailDto dto) => await service.CreateOrderDetail(dto);

    [HttpPut("{id}")]
    public async Task<Response<GetOrderDetailDto>> Update(int id, [FromBody] UpdateOrderDetailDto dto) => await service.UpdateOrderDetail(id, dto);

    [HttpDelete("{id}")]
    public async Task<Response<string>> Delete(int id) => await service.DeleteOrderDetail(id);
}