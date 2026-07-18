using System.Net;
using AutoMapper;
using Core.DTOs.MenuDto;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MenuService(IMenuRepository repository, IMapper mapper) : IMenuService
{
    public async Task<Response<List<GetMenuDto>>> GetMenus(MenuFilter filter)
    {
        var (menus, totalRecords) = await repository.GetPagedMenusAsync(filter);
        var data = mapper.Map<List<GetMenuDto>>(menus);
        return new PagedResponse<List<GetMenuDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetMenuDto>> GetMenuById(int id)
    {
        try
        {
            var menu = await repository.GetByIdAsync(id);
            if (menu is null)
            {
                return new Response<GetMenuDto>(HttpStatusCode.NotFound, "Menu not found");
            }

            var dto = mapper.Map<GetMenuDto>(menu);
            return new Response<GetMenuDto>(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Response<GetMenuDto>> CreateMenu(CreateMenuDto dto)
    {
        try
        {
            var menu = mapper.Map<Menu>(dto);
            await repository.AddAsync(menu);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<GetMenuDto>(HttpStatusCode.BadRequest, "Meny not added")
                : new Response<GetMenuDto>(mapper.Map<GetMenuDto>(menu));
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Response<GetMenuDto>> UpdateMenu(int id, UpdateMenuDto dto)
    {
        try
        {
            var menu = await repository.GetByIdAsync(id);
            if (menu is null)
            {
                return new Response<GetMenuDto>(HttpStatusCode.NotFound, "Menu not found");
            }

            mapper.Map(dto, menu);
            var result = await repository.SaveChangesAsync();
            
            return result == 0
                ? new Response<GetMenuDto>(HttpStatusCode.BadRequest, "Menu not updated")
                : new Response<GetMenuDto>(mapper.Map<GetMenuDto>(menu));
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("Failed to update menu.", e);
        }
    }

    public async Task<Response<string>> DeleteMenu(int id)
    {
        try
        {
            var menu = await repository.GetByIdAsync(id);
            if (menu is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Menu not found");
            }

            repository.Delete(menu);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Menu not deleted")
                : new Response<string>("Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}