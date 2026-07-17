namespace Core.Filters;

public class OrderDetailFilter
{
    public int? FromQuantity { get; set; }
    public int? ToQuantity { get; set; }
    public int? FromPrice { get; set; }
    public int? ToPrice { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}