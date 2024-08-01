using MediatR;
using TodoApp.Application.Common.Extensions;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Application.Features.TodoFeatures
{
    public sealed class SwitchTodoStatus
    {
        public record SwitchCommand(Guid Id) : IRequest;

        public class Handler(AppDbContext dbContext) : IRequestHandler<SwitchCommand>
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task Handle(SwitchCommand request, CancellationToken cancellationToken)
            {
                var todo = await _dbContext.Todos.FindNonNullableAsync(request.Id, cancellationToken);

                todo.Toggle();

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
