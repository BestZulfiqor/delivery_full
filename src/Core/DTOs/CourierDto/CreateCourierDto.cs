using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTOs.CourierDto;

public class CreateCourierDto
{
    public int UserId { get; set; }
    public CourierStatus CourierStatus { get; set; }
    public string? CurrentLocation { get; set; }
    [Range(1, 5)] public decimal Rating { get; set; }
    public TransportType TransportType { get; set; }
}