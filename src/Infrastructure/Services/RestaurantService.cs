using System.Net;
using AutoMapper;
using Core.DTOs.RestaurantDto;
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

public class RestaurantService(IRestaurantRepository repository, IMapper mapper) :
    IRestaurantService
{
    public async Task<Response<List<GetRestaurantDto>>> GetRestaurants(RestaurantFilter filter)
    {
        var (restaurants, totalRecords) = await repository.GetPagedRestaurantsAsync(filter);
        var data = mapper.Map<List<GetRestaurantDto>>(restaurants);
        return new PagedResponse<List<GetRestaurantDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetRestaurantDto>> GetRestaurantById(int id)
    {
        var restaurant = await repository.GetByIdAsync(id);
        if (restaurant is null)
        {
            return new Response<GetRestaurantDto>(HttpStatusCode.NotFound, "Restaurant not found");
        }

        return new Response<GetRestaurantDto>(mapper.Map<GetRestaurantDto>(restaurant));
    }

    public async Task<Response<GetRestaurantDto>> CreateRestaurant(CreateRestaurantDto dto)
    {
        try
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            await repository.AddAsync(restaurant);
            var result = await repository.SaveChangesAsync();

            return result == 0
                ? new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "Failed to create a restaurant")
                : new Response<GetRestaurantDto>(mapper.Map<GetRestaurantDto>(restaurant));
        }
        catch (Exception e)
        {
            throw new NotCreateException("Failed to save restaurant to DB", e);
        }
    }

    public async Task<Response<GetRestaurantDto>> UpdateRestaurant(int id, UpdateRestaurantDto dto)
    {
        try
        {
            var restaurant = await repository.GetByIdAsync(id);
            if (restaurant is null)
            {
                return new Response<GetRestaurantDto>(HttpStatusCode.NotFound, "Restaurant not found");
            }

            mapper.Map(dto, restaurant);
            repository.Update(restaurant);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "Restaurant not updated")
                : new Response<GetRestaurantDto>(mapper.Map<GetRestaurantDto>(restaurant));
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("FAILED TO UPDATE RESTAURANT.", e);
        }
    }

    public async Task<Response<string>> DeleteRestaurant(int id)
    {
        var restaurant = await repository.GetByIdAsync(id);
        if (restaurant is null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Restaurant not found");
        }

        repository.Delete(restaurant);
        var result = await repository.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Failed to delete")
            : new Response<string>("Deleted");

    }
}