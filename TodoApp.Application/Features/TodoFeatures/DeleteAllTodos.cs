using MediatR;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Application.Features.TodoFeatures
{
    public sealed class DeleteAllTodos
    {
        public record Command : IRequest;

        public class Handler(AppDbContext dbContext) : IRequestHandler<Command>
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _dbContext.Todos.RemoveRange(_dbContext.Todos);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
