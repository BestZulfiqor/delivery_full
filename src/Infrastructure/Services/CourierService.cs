using System.Net;
using AutoMapper;
using Core.DTOs.CourierDto;
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

public class CourierService(ICourierRepository repository, IMapper mapper) : ICourierService
{
    public async Task<Response<List<GetCourierDto>>> GetCouriers(CourierFilter filter)
    {
        var (couriers, totralRecords) = await repository.GetPagedCouriersAsync(filter);
        var data = mapper.Map<List<GetCourierDto>>(couriers);
        return new PagedResponse<List<GetCourierDto>>(data, filter.PageNumber, filter.PageSize, totralRecords);
    }

    public async Task<Response<GetCourierDto>> GetCourierById(int id)
    {
        var courier = await repository.GetByIdAsync(id);
        if (courier is not null)
        {
            return new Response<GetCourierDto>(HttpStatusCode.NotFound, "Courier not found");
        }

        return new Response<GetCourierDto>(mapper.Map<GetCourierDto>(courier));
    }

    public async Task<Response<GetCourierDto>> CreateCourier(CreateCourierDto dto)
    {
        try
        {
            var courier = mapper.Map<Courier>(dto);
            await repository.AddAsync(courier);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<GetCourierDto>(HttpStatusCode.BadRequest, "Failed to create")
                : new Response<GetCourierDto>(mapper.Map<GetCourierDto>(courier));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetCourierDto>> UpdateCourier(int id, UpdateCourierDto dto)
    {
        try
        {
            var courier = await repository.GetByIdAsync(id);
            if (courier is null)
            {
                return new Response<GetCourierDto>(HttpStatusCode.NotFound, "Courier not found");
            }

            mapper.Map(dto, courier);
            var result = await repository.SaveChangesAsync();
            
            return result == 0
                ? new Response<GetCourierDto>(HttpStatusCode.BadRequest, "Not update courier")
                : new Response<GetCourierDto>(mapper.Map<GetCourierDto>(courier));
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
            var courier = await repository.GetByIdAsync(id);
            if (courier is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Courier not found");
            }

            repository.Delete(courier);
            var result = await repository.SaveChangesAsync();
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