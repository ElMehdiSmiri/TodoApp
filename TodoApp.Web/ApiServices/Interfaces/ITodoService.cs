using TodoApp.Web.ApiModels;

namespace TodoApp.Web.ApiServices.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodos();
        Task<HttpResponseMessage> Create(TodoCreateDto body);
        Task<HttpResponseMessage> Edit(TodoEditDto body);
        Task<HttpResponseMessage> SwitchStatus(Guid id);
        Task<HttpResponseMessage> Delete(Guid id);
    }
}
