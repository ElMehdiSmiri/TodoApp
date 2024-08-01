namespace TodoApp.Web.ApiModels
{
    public class TodoEditDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
