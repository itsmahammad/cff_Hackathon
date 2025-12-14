using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Models.Table;
using CffHackathon.Application.Common.Services;
using CffHackathon.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CffHackathon.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TablesController : ControllerBase
{
    private readonly ITableService _tableService;

    public TablesController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        var tables = await _tableService.GetAllTablesAsync();
        return Ok(Response<List<TableReturnDto>>.Success(tables, 200));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTableById(int id)
    {
        var table = await _tableService.GetTableByIdAsync(id);
        return Ok(Response<TableReturnDto>.Success(table, 200));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTable([FromBody] TableCreateDto createDto)
    {
        await _tableService.CreateTableAsync(createDto);
        return Ok(Response<string>.Success("Table created successfully", 201));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveTable(int id)
    {
        await _tableService.RemoveTableAsync(id);
        return Ok(Response<string>.Success("Table deleted successfully", 200));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateTableStatus(int id, [FromBody] TableStatus status)
    {
        await _tableService.UpdateTableStatusAsync(id, status);
        return Ok(Response<string>.Success("Table status updated successfully", 200));
    }
}
