using Core.Enums;

namespace Core.DTOs.UserDto;

public class CreateUserDto
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserRole UserRole { get; set; }
}