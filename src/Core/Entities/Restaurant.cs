using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    [Range(1, 5)] public decimal Rating { get; set; }
    public string WorkingHours { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ContactPhone { get; set; } = null!;
    public bool IsActive { get; set; }
    public decimal MinOrderAmount { get; set; }
    public decimal DeliveryPrice { get; set; }

    public virtual ICollection<Menu> MenuItems { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}