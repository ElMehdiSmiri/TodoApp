namespace TodoApp.Application.Dtos
{
    public record TodoDto(
        Guid Id,
        string Title,
        string? Description,
        bool IsComplete);
}
