using Core.DTOs.MenuDto;
using Core.Filters;
using Core.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController(IMenuService service) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetMenuDto>>> GetAll([FromQuery] MenuFilter filter)
    {
        return await service.GetMenus(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<GetMenuDto>> GetById(int id)
    {
        return await service.GetMenuById(id);
    }
    
    [HttpPost]
    public async Task<Response<GetMenuDto>> Create([FromBody] CreateMenuDto dto)
    {
        return await service.CreateMenu(dto);
    }

    [HttpPut("{id}")]
    public async Task<Response<GetMenuDto>> Update(int id, [FromBody] UpdateMenuDto dto)
    {
        return await service.UpdateMenu(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteMenu(id);
    }
}