using System.Net;
using AutoMapper;
using Core.DTOs.OrderDto;
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

public class OrderService(IOrderRepository repository, IMapper mapper) : IOrderService
{
    public async Task<Response<List<GetOrderDto>>> GetOrders(OrderFilter filter)
    {
        var (orders, totalRecords) = await repository.GetPagedOrdersAsync(filter);
        var data = mapper.Map<List<GetOrderDto>>(orders);
        return new PagedResponse<List<GetOrderDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetOrderDto>> GetOrderById(int id)
    {
        try
        {
            var order = await repository.GetByIdAsync(id);
            if (order is null)
            {
                return new Response<GetOrderDto>(HttpStatusCode.NotFound, "Order not found");
            }

            var dto = mapper.Map<GetOrderDto>(order);
            return new Response<GetOrderDto>(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetOrderDto>> CreateOrder(CreateOrderDto dto)
    {
        try
        {
            var order = mapper.Map<Order>(dto);
            await repository.AddAsync(order);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Order not created")
                : new Response<GetOrderDto>(mapper.Map<GetOrderDto>(order));
        }
        catch (Exception e)
        {
            throw new NotCreateException("Failed to create an order", e);
        }
    }

    public async Task<Response<GetOrderDto>> UpdateOrder(int id, UpdateOrderDto dto)
    {
        try
        {
            var order = await repository.GetByIdAsync(id);
            if (order is null)
            {
                return new Response<GetOrderDto>(HttpStatusCode.NotFound, "Order not found");
            }

            mapper.Map(dto, order);
            var result = await repository.SaveChangesAsync();

            return result == 0
                ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Order not update")
                : new Response<GetOrderDto>(mapper.Map<GetOrderDto>(order));
        }
        catch (Exception e)
        {
            throw new NotUpdatedException("Failed to update an order", e);
        }
    }

    public async Task<Response<string>> DeleteOrder(int id)
    {
        try
        {
            var order = await repository.GetByIdAsync(id);
            if (order is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Order not delete");
            }

            repository.Delete(order);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Order not deleted")
                : new Response<string>("Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}