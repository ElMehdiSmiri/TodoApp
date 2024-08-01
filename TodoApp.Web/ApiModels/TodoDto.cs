namespace TodoApp.Web.ApiModels
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsComplete { get; set; }

        public TodoEditDto GetEditDto()
        {
            return new TodoEditDto
            {
                Id = Id,
                Title = Title,
                Description = Description
            };
        }
    }
}
