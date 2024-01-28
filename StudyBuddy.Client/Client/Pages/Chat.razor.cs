using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using SharedAPI.Data;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Chat
    {
        [CascadingParameter(Name ="AuthenticationState")]
        public AuthenticationState AuthenticationState { get; set; }
        [Parameter]
        public string User { get; set; }
        private HubConnection hubConnection;
        private List<MessagedPeople> onlineUsers = new List<MessagedPeople>();
        private MessagedPeople? otherUser = null;
        private List<HubMessage> messages = new List<HubMessage>();
        private string? message = String.Empty;

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            onlineUsers = await messageService.GetContacts();
        }

        protected override async Task OnParametersSetAsync()
        {
            if(User is not null)
            {
                await HandleUserSelected(User);
            }
        }

        public async Task BroadcastMessage()
        {
            try
            {
                await hubConnection.SendAsync("BroadcastMessage", otherUser.UserName, message);
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

            otherUser = onlineUsers.Where(x => x.UserName.Equals(SelectedUser)).FirstOrDefault();

            hubConnection = await messageService.ConfigureHubConnection(otherUser?.UserName);

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
