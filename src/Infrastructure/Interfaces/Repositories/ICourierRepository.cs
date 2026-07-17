using Core.Entities;
using Core.Filters;

namespace Infrastructure.Interfaces.Repositories;

public interface ICourierRepository : IBaseRepository<Courier>
{
    Task<(List<Courier> Data, int TotalRecords)> GetPagedCouriersAsync(CourierFilter filter);
}