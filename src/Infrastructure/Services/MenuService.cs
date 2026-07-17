using AutoMapper;
using Core.DTOs.MenuDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MenuService(DataContext context, IMapper mapper) : IMenuService
{
    public async Task<Response<List<GetMenuDto>>> GetMenus(MenuFilter filter)
    {
        try
        {
            var query = context.Menus.AsNoTracking().AsQueryable();
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
            var menus = await query.OrderBy(n => n.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var data = mapper.Map<List<GetMenuDto>>(menus);
            return new PagedResponse<List<GetMenuDto>>(
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

    public Task<Response<GetMenuDto>> GetMenuById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetMenuDto>> CreateMenu(CreateMenuDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetMenuDto>> UpdateMenu(int id, UpdateMenuDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> DeleteMenu(int id)
    {
        throw new NotImplementedException();
    }
}