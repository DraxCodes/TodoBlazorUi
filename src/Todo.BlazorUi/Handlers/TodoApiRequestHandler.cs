using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Todo.BlazorUi.Entities;

namespace Todo.BlazorUi.Handlers
{
    public class TodoApiRequestHandler
    {
        private readonly HttpClient _client;

        public TodoApiRequestHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> HandshakeSuccess(string apiUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiUrl}/api/about");
            var response = await _client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task AddTodoAsync(TodoItem todoItem, string apiUrl)
        {
            var json = JsonConvert.SerializeObject(todoItem);
            var request = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}/api/todo/add");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TodoItem[]> GetOrRefreshTodoItemsAsync(string apiUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiUrl}/api/todo/list");

            var httpResponse = await _client.SendAsync(request);
            httpResponse.EnsureSuccessStatusCode();

            var json = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TodoItem[]>(json);
        }

        public async Task DeleteTodosAsync(TodoItem[] todoItems, string apiUrl)
        {
            var pendingDeleteItems = todoItems.Where(i => i.IsCompleted == true);
            var itemsToDelete = pendingDeleteItems.Select(i => i.Id);

            var json = JsonConvert.SerializeObject(itemsToDelete);
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{apiUrl}/api/todo/removemany");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
