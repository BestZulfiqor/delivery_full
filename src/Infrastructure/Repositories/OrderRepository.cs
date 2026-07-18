using Core.Entities;
using Core.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(DataContext context) : BaseRepository<Order>(context), IOrderRepository
{
    public async Task<(List<Order> Data, int TotalRecords)> GetPagedOrdersAsync(OrderFilter filter)
    {
        var query = GetAll();
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

        var totalRecords = await query.CountAsync();
        var data = await query.OrderBy(n => n.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        return (data, totalRecords);
    }
}