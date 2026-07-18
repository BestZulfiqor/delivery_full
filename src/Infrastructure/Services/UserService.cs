using System.Net;
using AutoMapper;
using Core.DTOs.UserDto;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    public async Task<Response<List<GetUserDto>>> GetUsers(UserFilter filter)
    {
        var (users, totalRecords) = await repository.GetPagedUsersAsync(filter);
        var data = mapper.Map<List<GetUserDto>>(users);
        return new PagedResponse<List<GetUserDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetUserDto>> GetUserById(int id)
    {
        try
        {
            var user = await repository.GetByIdAsync(id);
            if (user is null)
            {
                return new Response<GetUserDto>(HttpStatusCode.NotFound, "User not found");
            }

            return new Response<GetUserDto>(mapper.Map<GetUserDto>(user));
        }
        catch (Exception e)
        {
            throw new("Cannot add find user by id", e);
        }
    }

    public async Task<Response<GetUserDto>> CreateUser(CreateUserDto dto)
    {
        try
        {
            var user = mapper.Map<User>(dto);
            await repository.AddAsync(user);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not added")
                : new Response<GetUserDto>(mapper.Map<GetUserDto>(user));
        }
        catch (DbUpdateException e)
        {
            throw new NotCreateException("Failed to create a user.", e);
        }
    }

    public async Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto dto)
    {
        try
        {
            var user = await repository.GetByIdAsync(id);
            if (user is null)
            {
                return new Response<GetUserDto>(HttpStatusCode.NotFound, "User not found");
            }

            mapper.Map(dto, user);
            var result = await repository.SaveChangesAsync();
            
            return result == 0
                ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not updated")
                : new Response<GetUserDto>(mapper.Map<GetUserDto>(user));
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("Failed to update a user", e);
        }
    }

    public async Task<Response<string>> DeleteUser(int id)
    {
        try
        {
            var user = await repository.GetByIdAsync(id);
            if (user is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "User not found");
            }

            repository.Delete(user);
            var result = await repository.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "User not deleted")
                : new Response<string>("Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}