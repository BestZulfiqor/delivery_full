using Core.DTOs.UserDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    public Task<Response<List<GetUserDto>>> GetUsers(UserFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetUserDto>> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetUserDto>> CreateUser(CreateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}