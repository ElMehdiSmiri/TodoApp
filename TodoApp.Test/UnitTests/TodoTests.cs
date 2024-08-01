using TodoApp.Domain.Entities;

namespace TodoApp.Test.UnitTests
{
    public class TodoTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var title = "Test Title";
            var description = "Test Description";

            // Act
            var todo = new Todo(title, description);

            // Assert
            Assert.Equal(title, todo.Title);
            Assert.Equal(description, todo.Description);
            Assert.False(todo.IsComplete);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenTitleIsNull()
        {
            // Arrange
            string? title = null;

            // Act & Assert
#pragma warning disable CS8604 // Possible null reference argument.
            var exception = Assert.Throws<ArgumentException>(() => new Todo(title, "Description"));
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.Equal("Title cannot be null or empty. (Parameter 'title')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenTitleIsEmpty()
        {
            // Arrange
            string title = "";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Todo(title, "Description"));
            Assert.Equal("Title cannot be null or empty. (Parameter 'title')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenTitleExceedsMaxLength()
        {
            // Arrange
            string title = new('a', 101);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Todo(title, "Description"));
            Assert.Equal("Title cannot exceed 100 characters. (Parameter 'title')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenDescriptionExceedsMaxLength()
        {
            // Arrange
            string description = new('a', 201);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Todo("Title", description));
            Assert.Equal("Description cannot exceed 200 characters. (Parameter 'description')", exception.Message);
        }

        [Fact]
        public void Update_ShouldUpdateTitleAndDescription()
        {
            // Arrange
            var todo = new Todo("Initial Title", "Initial Description");
            var newTitle = "Updated Title";
            var newDescription = "Updated Description";

            // Act
            todo.Update(newTitle, newDescription);

            // Assert
            Assert.Equal(newTitle, todo.Title);
            Assert.Equal(newDescription, todo.Description);
        }

        [Fact]
        public void Update_ShouldThrowArgumentException_WhenTitleIsInvalid()
        {
            // Arrange
            var todo = new Todo("Title", "Description");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => todo.Update("", "Description"));
        }

        [Fact]
        public void Update_ShouldThrowArgumentException_WhenDescriptionExceedsMaxLength()
        {
            // Arrange
            var todo = new Todo("Title", "Description");
            var longDescription = new string('a', 201);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => todo.Update("Title", longDescription));
            Assert.Equal("Description cannot exceed 200 characters. (Parameter 'description')", exception.Message);
        }

        [Fact]
        public void Toggle_ShouldChangeIsCompleteStatus()
        {
            // Arrange
            var todo = new Todo("Title", "Description");

            // Act
            todo.Toggle();

            // Assert
            Assert.True(todo.IsComplete);

            // Act
            todo.Toggle();

            // Assert
            Assert.False(todo.IsComplete);
        }
    }
}
