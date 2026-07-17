using System.Net;
using AutoMapper;
using Core.DTOs.UserDto;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using Core.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context, IMapper mapper) : IUserService
{
    public async Task<Response<List<GetUserDto>>> GetUsers(UserFilter filter)
    {
        try
        {
            var query = context.Users.AsNoTracking().AsQueryable();

            var totalRecords = await query.CountAsync();
            var users = await query.OrderBy(n => n.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var data = mapper.Map<List<GetUserDto>>(users);
            return new PagedResponse<List<GetUserDto>>(
                data,
                filter.PageNumber,
                filter.PageSize,
                totalRecords
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetUserDto>> GetUserById(int id)
    {
        try
        {
            var user = await context.Users.FindAsync(id);
            if (user is null)
            {
                return new Response<GetUserDto>(HttpStatusCode.NotFound, "Not found user");
            }

            var dto = mapper.Map<GetUserDto>(user);
            return new Response<GetUserDto>(dto);
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
            await context.Users.AddAsync(user);
            var result = await context.SaveChangesAsync();
            var getDto = mapper.Map<GetUserDto>(user);
            return result == 0
                ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not add")
                : new Response<GetUserDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotCreateException("FAILED TO SAVE USER TO DB.", e);
        }
    }

    public async Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto dto)
    {
        try
        {
            var user = await context.Users.FindAsync(id);
            if (user is null)
            {
                return new Response<GetUserDto>(HttpStatusCode.NotFound, "Not found");
            }

            mapper.Map(dto, user);
            var result = await context.SaveChangesAsync();

            var getDto = mapper.Map<GetUserDto>(user);
            
            return result == 0
                ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "Not update user")
                : new Response<GetUserDto>(getDto);
        }
        catch (DbUpdateException e)
        {
            throw new NotUpdatedException("FAILED TO UPDATE USER.", e);
        }
    }

    public async Task<Response<string>> DeleteUser(int id)
    {
        try
        {
            var user = await context.Users.FindAsync(id);
            if (user is null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Not found");
            }

            context.Users.Remove(user);
            var result = await context.SaveChangesAsync();
            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Not delete")
                : new Response<string>("Deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}