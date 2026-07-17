using System.Net;
using AutoMapper;
using Core.DTOs.CourierDto;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourierService(DataContext context, IMapper mapper) : ICourierService
{
    public async Task<Response<List<GetCourierDto>>> GetCouriers(CourierFilter filter)
    {
        try
        {
            var query = context.Couriers.AsNoTracking().AsQueryable();

            if (filter.CourierStatus is not null)
            {
                query = query.Where(x => x.CourierStatus == filter.CourierStatus);
            }

            if (filter.TransportType is not null)
            {
                query = query.Where(x => x.TransportType == filter.TransportType);
            }

            var totalRecords = await query.CountAsync();
            var couriers = await query.OrderBy(n => n.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var data = mapper.Map<List<GetCourierDto>>(couriers);
            return new PagedResponse<List<GetCourierDto>>(
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

    public async Task<Response<GetCourierDto>> GetCourierById(int id)
    {
        try
        {
            var courier = await context.Couriers.FindAsync(id);
            if (courier is null)
            {
                return new Response<GetCourierDto>(HttpStatusCode.NotFound, "Not found courier");
            }

            var dto = mapper.Map<GetCourierDto>(courier);
            return new Response<GetCourierDto>(dto);
        }
        catch (Exception e)
        {
            throw new("Cannot add find courier by id", e);
        }
    }

    public async Task<Response<GetCourierDto>> CreateCourier(CreateCourierDto dto)
    {
        try
        {
            var courier = mapper.Map<Courier>(dto);
            await context.Couriers.AddAsync(courier);
            var result = await context.SaveChangesAsync();
            var getDto = mapper.Map<GetCourierDto>(courier);
            return result == 0
                ? new Response<GetCourierDto>(HttpStatusCode.BadRequest, "Courier not add")
                : new Response<GetCourierDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotCreateException("FAILED TO SAVE COURIER TO DB.", e);
        }
    }

    public async Task<Response<GetCourierDto>> UpdateCourier(int id, UpdateCourierDto dto)
    {
        try
        {
            var courier = await context.Couriers.FindAsync(id);
            if (courier is null)
            {
                return new Response<GetCourierDto>(HttpStatusCode.NotFound, "Not found");
            }

            mapper.Map(dto, courier);
            var result = await context.SaveChangesAsync();

            var getDto = mapper.Map<GetCourierDto>(courier);
            
            return result == 0
                ? new Response<GetCourierDto>(HttpStatusCode.BadRequest, "Not update courier")
                : new Response<GetCourierDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("FAILED TO UPDATE COURIER.", e);
        }
    }

    public async Task<Response<string>> DeleteCourier(int id)
    {
        try
        {
            var courier = await context.Couriers.FindAsync(id);
            if (courier is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Not found");
            }

            context.Couriers.Remove(courier);
            var result = await context.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Not delete courier")
                : new Response<string>("Delete courier");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}