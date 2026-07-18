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
    public async Task<Response<List<GetMenuDto>>> GetAll([FromQuery] MenuFilter filter) => await service.GetMenus(filter);

    [HttpGet("{id}")]
    public async Task<Response<GetMenuDto>> GetById(int id) => await service.GetMenuById(id);

    [HttpPost]
    public async Task<Response<GetMenuDto>> Create([FromBody] CreateMenuDto dto) => await service.CreateMenu(dto);

    [HttpPut("{id}")]
    public async Task<Response<GetMenuDto>> Update(int id, [FromBody] UpdateMenuDto dto) => await service.UpdateMenu(id, dto);

    [HttpDelete("{id}")]
    public async Task<Response<string>> Delete(int id) => await service.DeleteMenu(id);
}