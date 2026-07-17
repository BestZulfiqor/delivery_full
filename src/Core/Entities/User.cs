using Core.Enums;

namespace Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserRole UserRole { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
    public virtual Courier CourierProfile { get; set; }
}