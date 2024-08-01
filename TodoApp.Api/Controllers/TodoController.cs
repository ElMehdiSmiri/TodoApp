using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoFeatures;

namespace TodoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoDto>>> Get(CancellationToken ct)
        {
            return Ok(await _mediator.Send(new GetTodos.Query(), ct));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoDto>> Get([FromRoute] Guid id, CancellationToken ct)
        {
            return Ok(await _mediator.Send(new GetTodoById.Query(id), ct));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoDto>> Post([FromBody] CreateTodo.CreateCommand command, CancellationToken ct)
        {
            return Ok(await _mediator.Send(command, ct));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoDto>> Put([FromRoute] Guid id, [FromBody] EditTodo.EditCommand command, CancellationToken ct)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await _mediator.Send(command, ct));
        }

        [HttpPatch("{id}/SwitchStatus")]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.Send(new SwitchTodoStatus.SwitchCommand(id), ct);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteTodo.Command(id), ct);

            return NoContent();
        }

        [HttpDelete("DeleteAll")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(CancellationToken ct)
        {
            await _mediator.Send(new DeleteAllTodos.Command(), ct);

            return NoContent();
        }
    }
}
