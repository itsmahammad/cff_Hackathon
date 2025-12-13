using CffHackathon.Application.Common.Models.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CffHackathon.Application.Common.Interfaces;
using CffHackathon.Application.Common.Models.Category;
using CffHackathon.Domain.Exceptions;

namespace CffHackathon.Application.Common.Services
{
    public interface IMenuItemService
    {
        Task<MenuItemReturnDto> GetMenuItemByIdAsync(int id);
        Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync();
        Task RemoveMenuItemAsync(int id);
        Task CreateMenuItemAsync(MenuItemCreateDto menuItemDto);
        Task GetMenuItemByCategoryId(int categoryId);
    }
    public class MenuItemService(IApplicationDbContext dbContext) : IMenuItemService
    {
        public async Task CreateMenuItemAsync(MenuItemCreateDto menuItemDto)
        {
            var categoryExsists = await dbContext.Categories.AnyAsync(c => c.Id == menuItemDto.CategoryId);
            if (!categoryExsists)
            {
                throw new NotFoundException($"Category with id {menuItemDto.CategoryId} not found.");
            }
            MenuItem menuItem = new MenuItem()
            {
                Name = menuItemDto.Name,
                Description = menuItemDto.Description,
                PhotoUrl = menuItemDto.PhotoUrl,
                Price = menuItemDto.Price,
                CategoryId = menuItemDto.CategoryId,
            };
            await dbContext.MenuItems.AddAsync(menuItem);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync()
        {
            var menuItems = await dbContext.MenuItems.Select(mi => new MenuItemReturnDto
            {
                Id = mi.Id,
                Name = mi.Name,
                Description = mi.Description,
                PhotoUrl = mi.PhotoUrl,
                Price = mi.Price,
                CategoryName = mi.Category.Name,
                IsAvailable = mi.IsAvailable
            }).ToListAsync();
            return menuItems;
        }

        public Task GetMenuItemByCategoryId(int categoryId)
        {
            var menuItems = dbContext.MenuItems.Where(mi => mi.CategoryId == categoryId)
                .Select(mi => new MenuItemReturnDto
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    Description = mi.Description,
                    PhotoUrl = mi.PhotoUrl,
                    Price = mi.Price,
                    CategoryName = mi.Category.Name,
                    IsAvailable = mi.IsAvailable
                }).ToListAsync();
            return menuItems;
        }

        public Task<MenuItemReturnDto> GetMenuItemByIdAsync(int id)
        {
            var menuItem = dbContext.MenuItems.Where(mi => mi.Id == id)
                .Select(mi => new MenuItemReturnDto
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    Description = mi.Description,
                    PhotoUrl = mi.PhotoUrl,
                    Price = mi.Price,
                    CategoryName = mi.Category.Name,
                    IsAvailable = mi.IsAvailable
                }).FirstOrDefaultAsync();
            if (menuItem == null)
            {
                throw new NotFoundException($"MenuItem with id {id} not found.");
            }
            return menuItem;
        }

        public async Task RemoveMenuItemAsync(int id)
        {
            var menuItem = await dbContext.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                throw new NotFoundException($"MenuItem with id {id} not found.");
            }
            dbContext.MenuItems.Remove(menuItem);
            await dbContext.SaveChangesAsync();
        }
    }
}
