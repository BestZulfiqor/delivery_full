using Core.Entities;
using Core.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderDetailRepository(DataContext context) : BaseRepository<OrderDetail>(context), IOrderDetailRepository
{
    public async Task<(List<OrderDetail> Data, int TotalRecords)> GetPagedOrderDetailsAsync(OrderDetailFilter filter)
    {
        var query = GetAll();
        if (filter.FromPrice is not null)
        {
            query = query.Where(x => x.Price >= filter.FromPrice);
        }

        if (filter.ToPrice is not null)
        {
            query = query.Where(x => x.Price <= filter.ToPrice);
        }

        var totalRecords = await query.CountAsync();
        var data = await query.OrderBy(n => n.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        return (data, totalRecords);
    }
}