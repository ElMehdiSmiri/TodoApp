using System.Net.Http.Json;
using TodoApp.Web.ApiModels;
using TodoApp.Web.ApiServices.Interfaces;

namespace TodoApp.Web.ApiServices
{
    public class TodoService(HttpClient httpClient) : ITodoService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _url = "api/Todo";

        public async Task<IEnumerable<TodoDto>> GetTodos()
        {
            return (await _httpClient.GetFromJsonAsync<IEnumerable<TodoDto>>(_url))!;
        }

        public async Task<HttpResponseMessage> Create(TodoCreateDto body)
        {
            return await _httpClient.PostAsJsonAsync(_url, body);
        }

        public async Task<HttpResponseMessage> Edit(TodoEditDto body)
        {
            return await _httpClient.PutAsJsonAsync(_url + $"/{body.Id}", body);
        }

        public async Task<HttpResponseMessage> SwitchStatus(Guid id)
        {
            return await _httpClient.PatchAsync(_url + $"/{id}/SwitchStatus", null);
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            return await _httpClient.DeleteAsync(_url + $"/{id}");
        }
    }
}
