using FluentValidation;
using MediatR;
using TodoApp.Application.Dtos;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Application.Features.TodoFeatures
{
    public sealed class CreateTodo
    {
        public record CreateCommand(string Title, string? Description) : IRequest<TodoDto>;

        public class Handler(AppDbContext dbContext) : IRequestHandler<CreateCommand, TodoDto>
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task<TodoDto> Handle(CreateCommand request, CancellationToken cancellationToken)
            {
                var todo = new Todo(request.Title, request.Description);

                await _dbContext.Todos.AddAsync(todo, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return todo.AsDto();
            }
        }

        public sealed class Validator : AbstractValidator<CreateCommand>
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
