using CffHackathon.Application.Common.Models.MenuItem;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CffHackathon.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemService _menuItem;

    public MenuItemsController(IMenuItemService menuItem)
    {
        _menuItem = menuItem;
    }

    // GET: api/MenuItems
    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems()
    {
        var menuItems = await _menuItem.GetAllMenuItemsAsync();
        return Ok(Response<List<MenuItemReturnDto>>.Success(menuItems, 200));
    }

    // GET: api/MenuItems/ByCategory/5
    [HttpGet("ByCategory/{categoryId:int}")]
    public async Task<IActionResult> GetMenuItemByCategoryId(int categoryId)
    {
        var items = await _menuItem.GetMenuItemByCategoryId(categoryId);
        return Ok(Response<List<MenuItemReturnDto>>.Success(items, 200));
    }

    // GET: api/MenuItems/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItemById(int id)
    {
        var menuItem = await _menuItem.GetMenuItemByIdAsync(id);
        return Ok(Response<MenuItemReturnDto>.Success(menuItem, 200));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveMenuItemAsync(int id)
    {
        await _menuItem.RemoveMenuItemAsync(id);
        return Ok(Response<string>.Success("Deleted successfully", 200));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItemAsync([FromBody] MenuItemCreateDto menuItemDto)
    {
        await _menuItem.CreateMenuItemAsync(menuItemDto);
        return Ok(Response<string>.Success("Created successfully", 201));
    }
}
