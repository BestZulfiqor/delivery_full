using Core.Entities;
using Core.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CourierRepository(DataContext context) : BaseRepository<Courier>(context), ICourierRepository
{
    public async Task<(List<Courier> Data, int TotalRecords)> GetPagedCouriersAsync(CourierFilter filter)
    {
        var query = GetAll();
        if (filter.CourierStatus is not null)
        {
            query = query.Where(x => x.CourierStatus == filter.CourierStatus);
        }

        if (filter.TransportType is not null)
        {
            query = query.Where(x => x.TransportType == filter.TransportType);
        }

        var totalRecords = await query.CountAsync();
        var data =await query.OrderBy(n => n.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        return (data, totalRecords);
    }
}