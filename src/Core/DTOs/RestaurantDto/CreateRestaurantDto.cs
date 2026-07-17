using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RestaurantDto;

public class CreateRestaurantDto
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    [Range(1, 5)] public decimal Rating { get; set; }
    public string WorkingHours { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ContactPhone { get; set; } = null!;
    public bool IsActive { get; set; }
    public decimal MinOrderAmount { get; set; }
    public decimal DeliveryPrice { get; set; }
}