using CffHackathon.Application.Common.Models.Category;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CffHackathon.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService category) : ControllerBase
{
    private readonly ICategoryService _category = category;

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto createDto)

    {
        await _category.AddCategoryAsync(createDto);
        var response = Response<string>.Success("Category successfully added", 201);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _category.GetAllCategoriesAsync();
        var response = Response<List<CategoryReturnDto>>.Success(categories, 200);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _category.GetCategoryAsync(id);
        var response = Response<CategoryReturnDto>.Success(category, 200);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveCategory(int categoryId)
    {
        await _category.RemoveCategoryAsync(categoryId);
        var response = Response<string>.Success("Category successfully removed", 200);
        return Ok(response);
    }
}
