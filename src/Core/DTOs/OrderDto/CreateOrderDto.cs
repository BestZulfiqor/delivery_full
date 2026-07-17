using Core.Enums;

namespace Core.DTOs.OrderDto;

public class CreateOrderDto
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int CourierId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}