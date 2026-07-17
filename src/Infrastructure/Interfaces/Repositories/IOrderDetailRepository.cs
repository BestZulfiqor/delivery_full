using Core.Entities;
using Core.Filters;

namespace Infrastructure.Interfaces.Repositories;

public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
{
    Task<(List<OrderDetail> Data, int TotalRecords)> GetPagedOrderDetailsAsync(OrderDetailFilter filter);
}