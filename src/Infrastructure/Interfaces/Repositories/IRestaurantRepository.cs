using Core.Entities;
using Core.Filters;

namespace Infrastructure.Interfaces.Repositories;

public interface IRestaurantRepository : IBaseRepository<Restaurant>
{
    Task<(List<Restaurant> Data, int TotalRecords)> GetPagedRestaurantsAsync(RestaurantFilter filter);
}