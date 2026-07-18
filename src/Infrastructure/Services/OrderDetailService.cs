using System.Net;
using AutoMapper;
using Core.DTOs.OrderDetailDto;
using Core.DTOs.OrderDetailDto;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderDetailService(IOrderDetailRepository repository, IMapper mapper) : IOrderDetailService
{
    public async Task<Response<List<GetOrderDetailDto>>> GetOrderDetails(OrderDetailFilter filter)
    {
        var (orderDetailts, totalRecords) = await repository.GetPagedOrderDetailsAsync(filter);
        var data = mapper.Map<List<GetOrderDetailDto>>(orderDetailts);
        return new PagedResponse<List<GetOrderDetailDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetOrderDetailDto>> GetOrderDetailById(int id)
    {
        try
        {
            var orderDetail = await repository.GetByIdAsync(id);
            if (orderDetail is null)
            {
                return new Response<GetOrderDetailDto>(HttpStatusCode.NotFound, "Order detail not found");
            }

            var dto = mapper.Map<GetOrderDetailDto>(orderDetail);
            return new Response<GetOrderDetailDto>(dto);
        }
        catch (Exception e)
        {
            throw new("Cannot add find orderDetail by id", e);
        }
    }

    public async Task<Response<GetOrderDetailDto>> CreateOrderDetail(CreateOrderDetailDto dto)
    {
        try
        {
            var orderDetail = mapper.Map<OrderDetail>(dto);
            await repository.AddAsync(orderDetail);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "Order detail not added")
                : new Response<GetOrderDetailDto>(mapper.Map<GetOrderDetailDto>(orderDetail));
        }
        catch (DbUpdateException e)
        {
            throw new NotCreateException("Failed to create an order detail", e);
        }
    }

    public async Task<Response<GetOrderDetailDto>> UpdateOrderDetail(int id, UpdateOrderDetailDto dto)
    {
        try
        {
            var orderDetail = await repository.GetByIdAsync(id);
            if (orderDetail is null)
            {
                return new Response<GetOrderDetailDto>(HttpStatusCode.NotFound, "Order detail found");
            }

            mapper.Map(dto, orderDetail);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "Order detail not update")
                : new Response<GetOrderDetailDto>(mapper.Map<GetOrderDetailDto>(orderDetail));
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("Failed to update an order detail.", e);
        }
    }

    public async Task<Response<string>> DeleteOrderDetail(int id)
    {
        try
        {
            var orderDetail = await repository.GetByIdAsync(id);
            if (orderDetail is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Order detail not found");
            }

            repository.Delete(orderDetail);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Order detail not deleted")
                : new Response<string>("Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}