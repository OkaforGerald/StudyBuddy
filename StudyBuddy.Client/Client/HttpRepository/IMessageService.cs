using Microsoft.AspNetCore.SignalR.Client;
using SharedAPI.Data;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface IMessageService
    {
        Task<HubConnection> ConfigureHubConnection(string? OtherUser = null);

        Task<List<MessagedPeople>> GetContacts();
    }
}
