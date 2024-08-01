namespace TodoApp.Web.ApiModels
{
    public class TodoCreateDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
