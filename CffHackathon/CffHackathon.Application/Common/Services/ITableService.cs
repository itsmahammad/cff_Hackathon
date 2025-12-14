using CffHackathon.Application.Common.Interfaces;
using CffHackathon.Application.Common.Models.Table;
using CffHackathon.Domain.Entities;
using CffHackathon.Domain.Enums;
using CffHackathon.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CffHackathon.Application.Common.Services;

public interface ITableService
{
    Task CreateTableAsync(TableCreateDto createDto);
    Task<List<TableReturnDto>> GetAllTablesAsync();
    Task<TableReturnDto> GetTableByIdAsync(int id);
    Task RemoveTableAsync(int id);
    Task UpdateTableStatusAsync(int id, TableStatus status);
}

public class TableService(IApplicationDbContext dbContext) : ITableService
{
    public async Task CreateTableAsync(TableCreateDto createDto)
    {
        var table = new Table
        {
            Number = createDto.Number,
            Capacity = createDto.Capacity,
            Status = TableStatus.Empty
        };
        await dbContext.Tables.AddAsync(table);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<TableReturnDto>> GetAllTablesAsync()
    {
        var tables = await dbContext.Tables
            .Select(t => new TableReturnDto
            {
                Id = t.Id,
                Number = t.Number,
                Capacity = t.Capacity,
                Status = t.Status
            })
            .ToListAsync();
        return tables;
    }

    public async Task<TableReturnDto> GetTableByIdAsync(int id)
    {
        var table = await dbContext.Tables
            .Where(t => t.Id == id)
            .Select(t => new TableReturnDto
            {
                Id = t.Id,
                Number = t.Number,
                Capacity = t.Capacity,
                Status = t.Status
            })
            .FirstOrDefaultAsync();
        
        if (table == null)
        {
            throw new NotFoundException($"Table with id {id} not found.");
        }
        
        return table;
    }

    public async Task RemoveTableAsync(int id)
    {
        var table = await dbContext.Tables.FindAsync(id);
        if (table == null)
        {
            throw new NotFoundException($"Table with id {id} not found.");
        }
        dbContext.Tables.Remove(table);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateTableStatusAsync(int id, TableStatus status)
    {
        var table = await dbContext.Tables.FindAsync(id);
        if (table == null)
        {
            throw new NotFoundException($"Table with id {id} not found.");
        }
        table.Status = status;
        await dbContext.SaveChangesAsync();
    }
}
