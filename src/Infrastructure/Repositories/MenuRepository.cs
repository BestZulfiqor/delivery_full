using Core.Entities;
using Core.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MenuRepository(DataContext context) : BaseRepository<Menu>(context), IMenuRepository
{
    public async Task<(List<Menu> Data, int TotalRecords)> GetPagedMenusAsync(MenuFilter filter)
    {
        var query = GetAll();

        if (filter.FromPreparationTime is not null)
        {
            query = query.Where(x => x.PreparationTime >= filter.FromPreparationTime);
        }

        if (filter.ToPreparationTime is not null)
        {
            query = query.Where(x => x.PreparationTime <= filter.ToPreparationTime);
        }

        if (filter.FromPrice is not null)
        {
            query = query.Where(x => x.Price >= filter.FromPrice);
        }

        if (filter.ToPrice is not null)
        {
            query = query.Where(x => x.Price <= filter.ToPrice);
        }

        if (filter.FromWeight is not null)
        {
            query = query.Where(x => x.Weight >= filter.FromWeight);
        }

        if (filter.ToWeight is not null)
        {
            query = query.Where(x => x.Weight <= filter.ToWeight);
        }

        if (filter.IsAvailable is not null)
        {
            query = query.Where(x => x.IsAvailable == filter.IsAvailable);
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        var totalRecords = await query.CountAsync();
        var data =await query.OrderBy(n => n.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        return (data, totalRecords);
    }
}