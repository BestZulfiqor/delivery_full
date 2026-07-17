namespace Core.Filters;

public class MenuFilter
{
    public string? Name { get; set; }
    public int? FromPrice { get; set; }
    public int? ToPrice { get; set; }
    public bool? IsAvailable { get; set; }
    public int? FromPreparationTime { get; set; }
    public int? ToPreparationTime { get; set; }
    public int? FromWeight { get; set; }
    public int? ToWeight { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}