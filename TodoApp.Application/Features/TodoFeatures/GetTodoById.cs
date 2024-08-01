using MediatR;
using TodoApp.Application.Common.Extensions;
using TodoApp.Application.Dtos;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Application.Features.TodoFeatures
{
    public sealed class GetTodoById
    {
        public record Query(Guid Id) : IRequest<TodoDto>;

        public class Handler(AppDbContext dbContext) : IRequestHandler<Query, TodoDto>
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task<TodoDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var todo = await _dbContext.Todos.FindNonNullableAsync(request.Id, cancellationToken);

                return todo.AsDto();
            }
        }
    }
}
