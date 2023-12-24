using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Chat
    {
        [CascadingParameter(Name ="AuthenticationState")]
        public AuthenticationState AuthenticationState { get; set; }
        private HubConnection hubConnection;
        private List<string> messages = new List<string>();
        private string? message;

        protected override async Task OnInitializedAsync()
        {
            var auth = await authService.RefreshToken();
            hubConnection = await messageService.ConfigureHubConnection();

            hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                messages.Add(message);
                StateHasChanged();
            });

            try
            {
                await hubConnection.StartAsync();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task BroadcastMessage()
        {
            await hubConnection.SendAsync("BroadcastMessage", message);
        }

        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            await hubConnection.DisposeAsync();
        }
}
}
