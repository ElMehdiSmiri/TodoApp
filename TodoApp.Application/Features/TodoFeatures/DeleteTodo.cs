using MediatR;
using TodoApp.Application.Common.Extensions;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Application.Features.TodoFeatures
{
    public sealed class DeleteTodo
    {
        public record Command(Guid Id) : IRequest;

        public class Handler(AppDbContext dbContext) : IRequestHandler<Command>
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var todo = await _dbContext.Todos.FindNonNullableAsync(request.Id, cancellationToken);

                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
