using CffHackathon.Application.Common.Models.MenuItem;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace CffHackathon.WebAPI.Controllers
{
    public class MenuItemController : Controller
    {
        IMenuItemService _menuItem;
        public MenuItemController(IMenuItemService menuItem)
        {
            _menuItem = menuItem;
        }
        public async Task<IActionResult> GetAllMenuItems()
        {
            var menuItems = await _menuItem.GetAllMenuItemsAsync();
            var response=Response<List<MenuItemReturnDto>>.Success( menuItems, 200);
            return Ok(response);
        }
        public async Task<IActionResult> GetMenuItemByCategoryId(int categoryId)
        {
            var category = await _menuItem.GetMenuItemByCategoryId(categoryId);
            var response = Response<MenuItemReturnDto>.Success(category, 200);
            return Ok(response);
        }
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuItem.GetMenuItemByIdAsync(id);
            var response = Response<MenuItemReturnDto>.Success(menuItem, 200);
            return Ok(response);
        }
        public async Task<string> RemoveMenuItemAsync(int id)
        {
            await _menuItem.RemoveMenuItemAsync(id);
            return "Deleted succsesfully";

        }
        public async Task<string> CreateMenuItemAsync(MenuItemCreateDto menuItemDto)
        {
            await _menuItem.CreateMenuItemAsync(menuItemDto);
            return "Created successfully";
        }
    }
}
