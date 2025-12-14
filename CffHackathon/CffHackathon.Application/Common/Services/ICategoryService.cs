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

    public interface ICategoryService
    {
        Task AddCategoryAsync(CategoryCreateDto createDto);
        Task<List<CategoryReturnDto>> GetAllCategoriesAsync();
        Task RemoveCategoryAsync(int categoryId);
        Task<CategoryReturnDto> GetCategoryAsync(int categoryId); 
    }

    public class CategoryService(IApplicationDbContext dbContext) : ICategoryService
    {
        public async Task AddCategoryAsync(CategoryCreateDto createDto)
        {
            Category category = new Category
            {
                Name = createDto.Name
            };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<CategoryReturnDto>> GetAllCategoriesAsync()
        {
            var categories = await dbContext.Categories
               .Select(c => new CategoryReturnDto
               {
                   Id = c.Id,
                   Name = c.Name,
                   MenuItems = c.MenuItems
                       .Select(mi => new MenuItemInCategoryDto
                       {
                           Id = mi.Id,
                           Name = mi.Name,
                           Description = mi.Description,
                           PhotoUrl = mi.PhotoUrl,
                           Price = mi.Price
                       })
                       .ToList()
               })
               .ToListAsync();
            return categories;
        }

        public async Task<CategoryReturnDto> GetCategoryAsync(int categoryId)
        {
            var category = await dbContext.Categories
               .Select(c => new CategoryReturnDto
               {
                   Id = c.Id,
                   Name = c.Name,
                   MenuItems = c.MenuItems
                       .Select(mi => new MenuItemInCategoryDto
                       {
                           Id = mi.Id,
                           Name = mi.Name,
                           Description = mi.Description,
                           PhotoUrl = mi.PhotoUrl,
                           Price = mi.Price
                       })
                       .ToList()
               })
               .FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }
            return category;
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            var category = await dbContext.Categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();

        }
    }
}
