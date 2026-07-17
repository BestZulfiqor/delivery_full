using Core.DTOs.MenuDto;
using Core.Filters;
using Core.Responses;

namespace Infrastructure.Interfaces;

public interface IMenuService
{
    Task<Response<List<GetMenuDto>>> GetMenus(MenuFilter filter);
    Task<Response<GetMenuDto>> GetMenuById(int id);
    Task<Response<GetMenuDto>> CreateMenu(CreateMenuDto dto);
    Task<Response<GetMenuDto>> UpdateMenu(int id, UpdateMenuDto dto);
    Task<Response<string>> DeleteMenu(int id);
}