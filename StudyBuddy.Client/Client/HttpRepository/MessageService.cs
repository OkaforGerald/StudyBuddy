using System.ComponentModel.Design.Serialization;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.SignalR.Client;
using SharedAPI.Data;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public class MessageService : IMessageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public MessageService(ILocalStorageService localStorageService, HttpClient client)
        {
            _localStorageService = localStorageService;
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<HubConnection> ConfigureHubConnection(string? otherUser)
        {
            string authToken = await _localStorageService.GetItemAsync<string>("authToken");

            HubConnection hubConnection = new HubConnectionBuilder()
               .WithUrl($"https://localhost:7011/chat?otherUser={otherUser}", options =>
               {
                   options.AccessTokenProvider = () => Task.FromResult(authToken);
               })
               .Build();

            return hubConnection;
        }

        public async Task<List<MessagedPeople>> GetContacts()
        {
            var response = await _client.GetAsync("messages/contacts");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var users = JsonSerializer.Deserialize<List<MessagedPeople>>(content, _options);
            return users;
        }
    }
}
