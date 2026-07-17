using Core.Entities;
using Core.Filters;

namespace Infrastructure.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<(List<User> Data, int TotalRecords)> GetPagedUsersAsync(UserFilter filter);
}