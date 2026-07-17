using Core.Entities;
using Core.Filters;

namespace Infrastructure.Interfaces.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<(List<Order> Data, int TotalRecords)> GetPagedOrdersAsync(OrderFilter filter);
}