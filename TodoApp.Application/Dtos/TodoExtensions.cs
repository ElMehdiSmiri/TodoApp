using TodoApp.Domain.Entities;

namespace TodoApp.Application.Dtos
{
    public static class TodoExtensions
    {
        public static TodoDto AsDto(this Todo todo)
        {
            return new TodoDto(
                todo.Id,
                todo.Title,
                todo.Description,
                todo.IsComplete);
        }
    }
}
