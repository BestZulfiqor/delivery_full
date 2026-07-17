using System.Net;
using AutoMapper;
using Core.DTOs.RestaurantDto;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RestaurantService(DataContext context, IMapper mapper) : IRestaurantService
{
    public async Task<Response<List<GetRestaurantDto>>> GetRestaurants(RestaurantFilter filter)
    {
        try
        {
            var query = context.Restaurants.AsNoTracking().AsQueryable();

            var totalRecords = await query.CountAsync();
            var restaurants = await query.OrderBy(n => n.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var data = mapper.Map<List<GetRestaurantDto>>(restaurants);
            return new PagedResponse<List<GetRestaurantDto>>(
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

    public async Task<Response<GetRestaurantDto>> GetRestaurantById(int id)
    {
        try
        {
            var restaurant = await context.Restaurants.FindAsync(id);
            if (restaurant is null)
            {
                return new Response<GetRestaurantDto>(HttpStatusCode.NotFound, "Not found restaurant");
            }

            var dto = mapper.Map<GetRestaurantDto>(restaurant);
            return new Response<GetRestaurantDto>(dto);
        }
        catch (Exception e)
        {
            throw new("Cannot add find restaurant by id", e);
        }
    }

    public async Task<Response<GetRestaurantDto>> CreateRestaurant(CreateRestaurantDto dto)
    {
        try
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            await context.Restaurants.AddAsync(restaurant);
            var result = await context.SaveChangesAsync();
            var getDto = mapper.Map<GetRestaurantDto>(restaurant);
            return result == 0
                ? new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "Restaurant not add")
                : new Response<GetRestaurantDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotCreateException("FAILED TO SAVE RESTAURANT TO DB.", e);
        }
    }

    public async Task<Response<GetRestaurantDto>> UpdateRestaurant(int id, UpdateRestaurantDto dto)
    {
        try
        {
            var restaurant = await context.Restaurants.FindAsync(id);
            if (restaurant is null)
            {
                return new Response<GetRestaurantDto>(HttpStatusCode.NotFound, "Not found");
            }

            mapper.Map(dto, restaurant);
            var result = await context.SaveChangesAsync();

            var getDto = mapper.Map<GetRestaurantDto>(restaurant);
            
            return result == 0
                ? new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "Not update restaurant")
                : new Response<GetRestaurantDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("FAILED TO UPDATE RESTAURANT.", e);
        }
    }

    public async Task<Response<string>> DeleteRestaurant(int id)
    {
        try
        {
            var restaurant = await context.Restaurants.FindAsync(id);
            if (restaurant is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Not found");
            }

            context.Restaurants.Remove(restaurant);
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