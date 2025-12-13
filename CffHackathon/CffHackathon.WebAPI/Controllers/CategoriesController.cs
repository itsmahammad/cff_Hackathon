using CffHackathon.Application.Common.Models.Category;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using CffHackathon.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CffHackathon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _category;
        public CategoriesController(ICategoryService category)
        {
            _category = category;
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCategory(CategoryCreateDto createDto)
        {
            await _category.AddCategoryAsync(createDto);
            var response = Response<string>.Success("Category successfully added",201);
            return Ok(response);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var categories= await _category.GetAllCategoriesAsync();
            var response = Response<List<CategoryReturnDto>>.Success(categories,200);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _category.GetCategoryAsync(id);
            var response = Response<CategoryReturnDto>.Success(category,200);
            return Ok(response);
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            await _category.RemoveCategoryAsync(categoryId);
            var response = Response<string>.Success("Category successfully removed",200);
            return Ok(response);
        }

    }
}
