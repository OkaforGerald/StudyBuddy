using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Services.Contracts;

namespace StudyBuddy.Hubs
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class ChatClient : Hub<IChatClient>
    {
        private readonly IServiceManager manager;

        public ChatClient(IServiceManager manager)
        {
            this.manager = manager;
        }

        public async Task BroadcastMessage(string otherUser, string message)
        {
            var currentUser = Context?.User?.Identity?.Name;
            var groupName = GetGroupName(currentUser, otherUser);

            var newMessage = await manager.MessageService.CreateMessage(currentUser, otherUser, message);

            await Clients.Group(groupName).ReceiveMessage(newMessage);
        }

        public override async Task OnConnectedAsync()
        {
            var otherUser = Context?.GetHttpContext()?.Request.Query["otherUser"].ToString();
            var currentUser = Context?.User?.Identity?.Name;

            var groupName = GetGroupName(currentUser, otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var messageThread = await manager.MessageService.GetThreadForUsers(currentUser, otherUser);

            await Clients.Caller.ReceiveThread(messageThread);

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
