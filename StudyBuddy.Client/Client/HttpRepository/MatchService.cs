using Blazored.LocalStorage;
using SharedAPI.Data;
using System.Text;
using System.Text.Json;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public class MatchService : IMatchService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public MatchService(ILocalStorageService localStorageService, HttpClient client)
        {
            _localStorageService = localStorageService;
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task CreateMatch(string username)
        {
            var bodyContent = new StringContent(username, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"match/{username}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task AckMatch(string username)
        {
            var bodyContent = new StringContent(username, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"match/{username}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

        public async Task DecMatch(string username)
        {
            var response = await _client.DeleteAsync($"match/{username}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }
    }
}
