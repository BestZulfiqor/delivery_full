using Core.Enums;

namespace Core.Filters;

public class OrderFilter : ValidFilter
{
    public OrderStatus? OrderStatus { get; set; }
    public DateTime? FromCreatedDate { get; set; }
    public DateTime? ToCreatedDate { get; set; }
}