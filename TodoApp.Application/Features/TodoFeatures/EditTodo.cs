using FluentValidation;
using MediatR;
using TodoApp.Application.Common.Extensions;
using TodoApp.Application.Dtos;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Application.Features.TodoFeatures
{
    public sealed class EditTodo
    {
        public record EditCommand(Guid Id,
            string Title,
            string Description) : IRequest<TodoDto>;

        public class Handler(AppDbContext dbContext) : IRequestHandler<EditCommand, TodoDto>
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task<TodoDto> Handle(EditCommand request, CancellationToken cancellationToken)
            {
                var todo = await _dbContext.Todos.FindNonNullableAsync(request.Id, cancellationToken);

                todo.Update(request.Title, request.Description);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return todo.AsDto();
            }
        }

        public sealed class Validator : AbstractValidator<EditCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Title)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(100);

                RuleFor(x => x.Description)
                    .MaximumLength(200);
            }
        }
    }
}
