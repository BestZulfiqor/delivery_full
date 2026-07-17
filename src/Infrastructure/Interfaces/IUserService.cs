using Core.DTOs.UserDto;
using Core.Filters;
using Core.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<GetUserDto>>> GetUsers(UserFilter filter);
    Task<Response<GetUserDto>> GetUserById(int id);
    Task<Response<GetUserDto>> CreateUser(CreateUserDto dto);
    Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto dto);
    Task<Response<string>> DeleteUser(int id);
}