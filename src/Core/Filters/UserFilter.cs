using Core.Enums;

namespace Core.Filters;

public class UserFilter
{
    public string? Name { get; set; }
    public DateTime? FromRegistrationDate { get; set; }
    public DateTime? ToRegistrationDate { get; set; }
    public UserRole? UserRole { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}