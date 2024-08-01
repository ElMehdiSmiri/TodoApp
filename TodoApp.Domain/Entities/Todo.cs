using TodoApp.Domain.Common;

namespace TodoApp.Domain.Entities
{
    public class Todo : BaseEntity
    {
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsComplete { get; private set; } = false;

        public Todo(string title, string? description)
        {
            ValidateTitle(title);
            ValidateDescription(description);

            Title = title;
            Description = description;
        }

        public void Update(string title, string? description)
        {
            ValidateTitle(title);
            ValidateDescription(description);

            Title = title;
            Description = description;
        }

        public void Toggle()
        {
            IsComplete = !IsComplete;
        }

        public static void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }
            if (title.Length > 100)
            {
                throw new ArgumentException("Title cannot exceed 100 characters.", nameof(title));
            }
        }

        public static void ValidateDescription(string? description)
        {
            if (description != null && description.Length > 200)
            {
                throw new ArgumentException("Description cannot exceed 200 characters.", nameof(description));
            }
        }
    }
}
