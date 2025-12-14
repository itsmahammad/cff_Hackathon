using CffHackathon.Application.Common.Models.MenuItem;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace CffHackathon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MenuItemController : Controller
    {
        IMenuItemService _menuItem;
        public MenuItemController(IMenuItemService menuItem)
        {
            _menuItem = menuItem;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var menuItems = await _menuItem.GetAllMenuItemsAsync();
            var response = Response<List<MenuItemReturnDto>>.Success(menuItems, 200);
            return Ok(response);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetMenuItemByCategoryId(int categoryId)
        {
            var category = await _menuItem.GetMenuItemByCategoryId(categoryId);
            var response = Response<List<MenuItemReturnDto>>.Success(category, 200);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuItem.GetMenuItemByIdAsync(id);
            var response = Response<MenuItemReturnDto>.Success(menuItem, 200);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<string> RemoveMenuItemAsync(int id)
        {
            await _menuItem.RemoveMenuItemAsync(id);
            return "Deleted succsesfully";

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<string> CreateMenuItemAsync(MenuItemCreateDto menuItemDto)
        {
            await _menuItem.CreateMenuItemAsync(menuItemDto);
            return "Created successfully";
        }
    }
}
