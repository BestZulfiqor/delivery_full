namespace Core.Filters;

public class RestaurantFilter
{
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public int? FromMinOrderAmount { get; set; }
    public int? ToMinOrderAmount { get; set; }
    public int? FromDeliveryPrice { get; set; }
    public int? ToDeliveryPrice { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}