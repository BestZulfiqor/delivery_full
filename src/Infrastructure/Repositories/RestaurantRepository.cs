using Core.Entities;
using Core.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RestaurantRepository(DataContext context) : BaseRepository<Restaurant>(context), IRestaurantRepository
{
    public async Task<(List<Restaurant> Data, int TotalRecords)> GetPagedRestaurantsAsync(RestaurantFilter filter)
    {
        var query = DbSet.AsNoTracking();
        if (filter.FromDeliveryPrice is not null)
        {
            query = query.Where(x => x.DeliveryPrice >= filter.FromDeliveryPrice);
        }

        if (filter.ToDeliveryPrice is not null)
        {
            query = query.Where(x => x.DeliveryPrice <= filter.ToDeliveryPrice);
        }

        if (filter.FromMinOrderAmount is not null)
        {
            query = query.Where(x => x.MinOrderAmount >= filter.FromMinOrderAmount);
        }

        if (filter.ToMinOrderAmount is not null)
        {
            query = query.Where(x => x.MinOrderAmount <= filter.ToMinOrderAmount);
        }

        if (filter.IsActive is not null)
        {
            query = query.Where(x => x.IsActive == filter.IsActive);
        }

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }

        var totalRecords = await query.CountAsync();

        var data = await query.OrderBy(n => n.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (data, totalRecords);
    }
}