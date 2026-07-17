using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Entities;

public class Courier
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public CourierStatus CourierStatus { get; set; }
    public string? CurrentLocation { get; set; }
    [Range(1, 5)] public decimal Rating { get; set; }
    public TransportType TransportType { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}