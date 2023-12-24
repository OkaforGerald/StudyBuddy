using Microsoft.AspNetCore.SignalR;

namespace StudyBuddy.Hubs
{
    public class ChatUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection?.User?.Identity?.Name;
        }
    }
}
