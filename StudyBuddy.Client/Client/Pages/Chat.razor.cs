using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Data_Transfer;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Chat
    {
        [CascadingParameter(Name ="AuthenticationState")]
        public AuthenticationState AuthenticationState { get; set; }
        private HubConnection hubConnection;
        private List<string> onlineUsers = new List<string>();
        private string? otherUser = null;
        private List<HubMessage> messages = new List<HubMessage>();
        private string? message = String.Empty;

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            onlineUsers = await messageService.GetContacts();
        }

        public async Task BroadcastMessage()
        {
            try
            {
                await hubConnection.SendAsync("BroadcastMessage", otherUser, message);
                message = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;

        private async Task HandleUserSelected(string SelectedUser)
        {
            if(hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }

            otherUser = SelectedUser;

            hubConnection = await messageService.ConfigureHubConnection(otherUser);

            hubConnection.On<HubMessage>("ReceiveMessage", (message) =>
            {
                messages.Add(message);
                StateHasChanged();
            });

            hubConnection.On<List<HubMessage>>("ReceiveThread", (Thread) =>
            {
                messages = Thread;
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
            if (hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }
        }
}
}
