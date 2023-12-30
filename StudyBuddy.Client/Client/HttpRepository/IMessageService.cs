using Microsoft.AspNetCore.SignalR.Client;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface IMessageService
    {
        Task<HubConnection> ConfigureHubConnection(string? OtherUser = null);

        Task<List<string>> GetContacts();
    }
}
