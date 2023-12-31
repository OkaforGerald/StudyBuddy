using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Shared;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Chat
    {
        [CascadingParameter(Name ="AuthenticationState")]
        public AuthenticationState AuthenticationState { get; set; }
        private HubConnection hubConnection;
        private List<string> onlineUsers = new List<string>();
        private string? otherUser = null;
        private List<Message> messages = new List<Message>();
        private string? message;

        protected override async Task OnInitializedAsync()
        {
            //var auth = await authService.RefreshToken();

            Interceptor.RegisterEvent();
            onlineUsers = await messageService.GetContacts();
        }

        public async Task BroadcastMessage()
        {
            await hubConnection.SendAsync("BroadcastMessage",otherUser, message);
        }

        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;

        private async Task HandleUserSelected(string SelectedUser)
        {
            otherUser = SelectedUser;

            hubConnection = await messageService.ConfigureHubConnection(otherUser);

            hubConnection.On<Message>("ReceiveMessage", (message) =>
            {
                messages.Add(message);
                StateHasChanged();
            });

            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public async ValueTask DisposeAsync()
        {
            Interceptor.DisposeEvent();
            await hubConnection.DisposeAsync();
        }
}
}
