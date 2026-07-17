namespace Core.DTOs.OrderDetailDto;

public class GetOrderDetailDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? SpecialInstructions { get; set; }
}