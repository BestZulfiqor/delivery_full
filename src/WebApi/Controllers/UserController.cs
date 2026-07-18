using Core.DTOs.UserDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetUserDto>>> GetAll([FromQuery] UserFilter filter) => await service.GetUsers(filter);

    [HttpGet("{id}")]
    public async Task<Response<GetUserDto>> GetUser(int id) => await service.GetUserById(id);

    [HttpPost]
    public async Task<Response<GetUserDto>> Create([FromBody] CreateUserDto dto) => await service.CreateUser(dto);

    [HttpPut("{id}")]
    public async Task<Response<GetUserDto>> Update(int id, [FromBody] UpdateUserDto dto) => await service.UpdateUser(id, dto);

    [HttpDelete("{id}")]
    public async Task<Response<string>> Delete(int id) => await service.DeleteUser(id);
}