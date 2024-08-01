using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Dtos;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Application.Features.TodoFeatures
{
    public sealed class GetTodos
    {
        public record Query : IRequest<IEnumerable<TodoDto>>;

        public class Handler(AppDbContext dbContext) : IRequestHandler<Query, IEnumerable<TodoDto>>
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task<IEnumerable<TodoDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todos = await _dbContext.Todos
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return todos.Select(x => x.AsDto()!);
            }
        }
    }
}
