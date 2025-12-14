using CffHackathon.Domain.Enums;

namespace CffHackathon.Application.Common.Models.Table;

public class TableCreateDto
{
    public string Number { get; set; }
    public int Capacity { get; set; }
}

public class TableReturnDto
{
    public int Id { get; set; }
    public string Number { get; set; }
    public int Capacity { get; set; }
    public TableStatus Status { get; set; }
}
