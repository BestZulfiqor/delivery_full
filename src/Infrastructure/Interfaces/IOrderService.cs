using Core.DTOs.OrderDto;
using Core.Filters;
using Core.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    Task<Response<List<GetOrderDto>>> GetOrders(OrderFilter filter);
    Task<Response<GetOrderDto>> GetOrderById(int id);
    Task<Response<GetOrderDto>> CreateOrder(CreateOrderDto dto);
    Task<Response<GetOrderDto>> UpdateOrder(int id, UpdateOrderDto dto);
    Task<Response<string>> DeleteOrder(int id);
}