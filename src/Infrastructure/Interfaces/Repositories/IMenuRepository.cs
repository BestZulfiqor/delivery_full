using Core.Entities;
using Core.Filters;

namespace Infrastructure.Interfaces.Repositories;

public interface IMenuRepository : IBaseRepository<Menu>
{
    Task<(List<Menu> Data, int TotalRecords)> GetPagedMenusAsync(MenuFilter filter);
}