using System.Net;
using AutoMapper;
using Core.DTOs.OrderDto;
using Core.Entities;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService(DataContext context, IMapper mapper) : IOrderService
{
    public async Task<Response<List<GetOrderDto>>> GetOrders(OrderFilter filter)
    {
        var query = context.Orders.AsNoTracking().AsQueryable();
        if (filter.FromCreatedDate is not null)
        {
            query = query.Where(x => x.CreatedAt >= filter.FromCreatedDate);
        }

        if (filter.ToCreatedDate is not null)
        {
            query = query.Where(x => x.CreatedAt <= filter.ToCreatedDate);
        }

        if (filter.OrderStatus is not null)
        {
            query = query.Where(x => x.OrderStatus == filter.OrderStatus);
        }

        var totalRecors = await query.CountAsync();
        var orders = await query.OrderBy(order => order.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        var data = mapper.Map<List<GetOrderDto>>(orders);
        return new PagedResponse<List<GetOrderDto>>(data, filter.PageNumber, filter.PageSize, totalRecors);
    }

    public async Task<Response<GetOrderDto>> GetOrderById(int id)
    {
        try
        {
            var order = await context.Orders.FindAsync(id);
            if (order is null)
            {
                return new Response<GetOrderDto>(HttpStatusCode.NotFound, "Not found");
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
            await context.Orders.AddAsync(order);
            var result = await context.SaveChangesAsync();
            var response = mapper.Map<GetOrderDto>(order);
            return result == 0
                ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Not created")
                : new Response<GetOrderDto>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetOrderDto>> UpdateOrder(int id, UpdateOrderDto dto)
    {
        try
        {
            var order = await context.OrderDetails.FindAsync(id);
            if (order is null)
            {
                return new Response<GetOrderDto>(HttpStatusCode.NotFound, "Not found order");
            }

            mapper.Map(dto, order);
            var response = mapper.Map<GetOrderDto>(order);
            var result = await context.SaveChangesAsync();

            return result == 0
                ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Not update")
                : new Response<GetOrderDto>(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<string>> DeleteOrder(int id)
    {
        try
        {
            var order = await context.Orders.FindAsync(id);
            if (order is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Not delete");
            }

            context.Orders.Remove(order);
            var result = await context.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Not delete")
                : new Response<string>("Delete");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}