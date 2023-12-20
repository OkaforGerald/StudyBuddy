using Microsoft.AspNetCore.SignalR.Client;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Chat
    {
        private HubConnection hubConnection;
        private List<string> messages = new List<string>();
        private string? message;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7011/chat")
                .Build();

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
