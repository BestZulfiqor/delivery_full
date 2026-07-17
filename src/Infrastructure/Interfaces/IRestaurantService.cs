using Core.DTOs.RestaurantDto;
using Core.Filters;
using Core.Responses;

namespace Infrastructure.Interfaces;

public interface IRestaurantDto
{
    Task<Response<List<GetRestaurantDto>>> GetRestaurants(RestaurantFilter filter);
    Task<Response<GetRestaurantDto>> GetRestaurantById(int id);
    Task<Response<GetRestaurantDto>> CreateRestaurant(CreateRestaurantDto dto);
    Task<Response<GetRestaurantDto>> UpdateRestaurant(int id, UpdateRestaurantDto dto);
    Task<Response<string>> DeleteRestaurant(int id);
}