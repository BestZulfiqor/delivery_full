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
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderDetailService(DataContext context, IMapper mapper) : IOrderDetailService
{
    public async Task<Response<List<GetOrderDetailDto>>> GetOrderDetails(OrderDetailFilter filter)
    {
        try
        {
            var query = context.OrderDetails.AsNoTracking().AsQueryable();
            if (filter.FromPrice is not null)
            {
                query = query.Where(x => x.Price >= filter.FromPrice);
            }

            if (filter.ToPrice is not null)
            {
                query = query.Where(x => x.Price <= filter.ToPrice);
            }

            var totalRecords = await query.CountAsync();
            var orderDetails = await query.OrderBy(n => n.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var data = mapper.Map<List<GetOrderDetailDto>>(orderDetails);
            return new PagedResponse<List<GetOrderDetailDto>>(
                data,
                filter.PageNumber,
                filter.PageSize,
                totalRecords
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetOrderDetailDto>> GetOrderDetailById(int id)
    {
        try
        {
            var orderDetail = await context.OrderDetails.FindAsync(id);
            if (orderDetail is null)
            {
                return new Response<GetOrderDetailDto>(HttpStatusCode.NotFound, "Not found");
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
            await context.OrderDetails.AddAsync(orderDetail);
            var result = await context.SaveChangesAsync();
            var getDto = mapper.Map<GetOrderDetailDto>(orderDetail);
            return result == 0
                ? new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "OrderDetail not add")
                : new Response<GetOrderDetailDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotCreateException("FAILED TO SAVE ORDERDETAIL TO DB.", e);
        }
    }

    public async Task<Response<GetOrderDetailDto>> UpdateOrderDetail(int id, UpdateOrderDetailDto dto)
    {
        try
        {
            var orderDetail = await context.OrderDetails.FindAsync(id);
            if (orderDetail is null)
            {
                return new Response<GetOrderDetailDto>(HttpStatusCode.NotFound, "Not found");
            }

            mapper.Map(dto, orderDetail);
            var result = await context.SaveChangesAsync();

            var getDto = mapper.Map<GetOrderDetailDto>(orderDetail);
            
            return result == 0
                ? new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "Not update orderDetail")
                : new Response<GetOrderDetailDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("FAILED TO UPDATE ORDERDETAIL.", e);
        }
    }

    public async Task<Response<string>> DeleteOrderDetail(int id)
    {
        try
        {
            var orderDetail = await context.OrderDetails.FindAsync(id);
            if (orderDetail is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Not found");
            }

            context.OrderDetails.Remove(orderDetail);
            var result = await context.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Not delete")
                : new Response<string>("Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}