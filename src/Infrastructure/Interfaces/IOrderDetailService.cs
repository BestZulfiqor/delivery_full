using Core.DTOs.OrderDetailDto;
using Core.Filters;
using Core.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderDetailService
{
    Task<Response<List<GetOrderDetailDto>>> GetOrderDetails(OrderDetailFilter filter);
    Task<Response<GetOrderDetailDto>> GetOrderDetailById(int id);
    Task<Response<GetOrderDetailDto>> CreateOrderDetail(CreateOrderDetailDto dto);
    Task<Response<GetOrderDetailDto>> UpdateOrderDetail(int id, UpdateOrderDetailDto dto);
    Task<Response<string>> DeleteOrderDetail(int id);
}