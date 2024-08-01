using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.Api.Controllers;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoFeatures;

namespace TodoApp.Test.FunctionalTests;

public class TodoControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TodoController _controller;

    public TodoControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TodoController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnTodos_WhenTodosExist()
    {
        // Arrange
        var todos = new List<TodoDto> { new(Guid.NewGuid(), "Title", "Description", false) };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTodos.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(todos);

        // Act
        var result = await _controller.Get(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<TodoDto>>(okResult.Value);
        Assert.Equal(todos, returnValue);
    }

    [Fact]
    public async Task Get_ShouldReturnTodo_WhenTodoExists()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new TodoDto(Guid.NewGuid(), "Title", "Description", false);
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTodoById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(todo);

        // Act
        var result = await _controller.Get(todoId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<TodoDto>(okResult.Value);
        Assert.Equal(todo, returnValue);
    }

    [Fact]
    public async Task Post_ShouldReturnCreatedTodo_WhenCommandIsValid()
    {
        // Arrange
        var todo = new TodoDto(Guid.NewGuid(), "Title", "Description", false);
        var command = new CreateTodo.CreateCommand("Title", "Description");

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(todo);

        // Act
        var result = await _controller.Post(command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<TodoDto>(okResult.Value);
        Assert.Equal(todo, returnValue);
    }

    [Fact]
    public async Task Put_ShouldReturnUpdatedTodo_WhenCommandIsValid()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new TodoDto(todoId, "Title", "Description", false);
        var command = new EditTodo.EditCommand(todoId, "Title", "Description");
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(todo);

        // Act
        var result = await _controller.Put(todoId, command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<TodoDto>(okResult.Value);
        Assert.Equal(todo, returnValue);
    }

    [Fact]
    public async Task Put_ShouldReturnBadRequest_WhenIdMismatch()
    {
        // Arrange
        var command = new EditTodo.EditCommand(Guid.NewGuid(), "Title", "Description");

        // Act
        var result = await _controller.Put(Guid.NewGuid(), command, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task SwitchStatus_ShouldReturnOk_WhenCommandIsValid()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<SwitchTodoStatus.SwitchCommand>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await _controller.Put(Guid.NewGuid(), CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(It.IsAny<SwitchTodoStatus.SwitchCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenTodoIsDeleted()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTodo.Command>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await _controller.Delete(todoId, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteTodo.Command>(), It.IsAny<CancellationToken>()), Times.Once);
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }

    [Fact]
    public async Task DeleteAll_ShouldReturnNoContent_WhenAllTodosAreDeleted()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAllTodos.Command>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await _controller.Delete(CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteAllTodos.Command>(), It.IsAny<CancellationToken>()), Times.Once);
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }
}