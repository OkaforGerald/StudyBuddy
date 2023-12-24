using System.ComponentModel.Design.Serialization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.SignalR.Client;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public class MessageService : IMessageService
    {
        private readonly ILocalStorageService _localStorageService;

        public MessageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<HubConnection> ConfigureHubConnection(string? OtherUser = null)
        {
            string authToken = await _localStorageService.GetItemAsync<string>("authToken");

            //HubConnection hubConnection = new HubConnectionBuilder()
            //   .WithUrl($"https://localhost:7011/chat?access_token={authToken}", options =>
            //   {
            //       options.AccessTokenProvider = () => Task.FromResult(authToken);
            //   })
            //   .Build();

            HubConnection hubConnection = new HubConnectionBuilder()
               .WithUrl($"https://localhost:7011/chat", options =>
               {
                   options.AccessTokenProvider = () => Task.FromResult(authToken);
               })
               .Build();

            return hubConnection;
        }
    }
}
