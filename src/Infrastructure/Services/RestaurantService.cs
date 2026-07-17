using Core.DTOs.RestaurantDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class RestaurantService : IRestaurantService
{
    public Task<Response<List<GetRestaurantDto>>> GetRestaurants(RestaurantFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetRestaurantDto>> GetRestaurantById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetRestaurantDto>> CreateRestaurant(CreateRestaurantDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetRestaurantDto>> UpdateRestaurant(int id, UpdateRestaurantDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> DeleteRestaurant(int id)
    {
        throw new NotImplementedException();
    }
}