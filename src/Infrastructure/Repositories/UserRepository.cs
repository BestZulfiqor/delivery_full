using Core.Entities;
using Core.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(DataContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<(List<User> Data, int TotalRecords)> GetPagedUsersAsync(UserFilter filter)
    {
        var query = GetAll();
        if (filter.FromRegistrationDate is not null)
        {
            query = query.Where(x => x.RegistrationDate >= filter.FromRegistrationDate);
        }

        if (filter.ToRegistrationDate is not null)
        {
            query = query.Where(x => x.RegistrationDate <= filter.ToRegistrationDate);
        }

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        var totalRecords = await query.CountAsync();
        var data = await query.OrderBy(n => n.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        return (data, totalRecords);
    }
}