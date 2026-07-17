using Core.DTOs.OrderDetailDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class OrderDetailService : IOrderDetailService
{
    public Task<Response<List<GetOrderDetailDto>>> GetOrderDetails(OrderDetailFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetOrderDetailDto>> GetOrderDetailById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetOrderDetailDto>> CreateOrderDetail(CreateOrderDetailDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetOrderDetailDto>> UpdateOrderDetail(int id, UpdateOrderDetailDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> DeleteOrderDetail(int id)
    {
        throw new NotImplementedException();
    }
}