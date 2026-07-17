using Core.Enums;

namespace Core.Filters;

public class CourierFilter : ValidFilter
{
    public CourierStatus? CourierStatus { get; set; }
    public TransportType? TransportType { get; set; }
}