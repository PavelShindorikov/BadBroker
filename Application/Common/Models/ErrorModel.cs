namespace Application.Common.Models;

public class ErrorModel
{
    public int Status { get; init; }
    public string Message { get; init; } = string.Empty!;
    public string Description { get; init; } = string.Empty!;
}