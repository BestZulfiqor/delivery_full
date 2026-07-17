using Core.Enums;

namespace Core.Filters;

public class CourierFilter : BaseFilter
{
    public CourierStatus? CourierStatus { get; set; }
    public TransportType? TransportType { get; set; }
}