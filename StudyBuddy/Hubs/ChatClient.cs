using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Shared;

namespace StudyBuddy.Hubs
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class ChatClient : Hub<IChatClient>
    {
        public async Task BroadcastMessage(string otherUser, string message)
        {
            var currentUser = Context?.User?.Identity?.Name;
            var groupName = GetGroupName(currentUser, otherUser);

            await Clients.Group(groupName).ReceiveMessage($"{currentUser} - {message}");
        }

        public async Task SendToIndividual(string connectionId, string message)
        {
            await Clients.Client(connectionId).ReceiveMessage(message);
        }

        public override async Task OnConnectedAsync()
        {
            var otherUser = Context?.GetHttpContext()?.Request.Query["otherUser"].ToString();
            var currentUser = Context?.User?.Identity?.Name;

            var groupName = GetGroupName(currentUser, otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            //return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        private string GetGroupName(string currentUser, string otherUser)
        {
            var groupName = String.CompareOrdinal(currentUser, otherUser) > 0 ? $"{currentUser}-{otherUser}" : $"{otherUser}-{currentUser}";

            return groupName;
        }
    }
}
