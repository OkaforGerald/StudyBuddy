using Entities.Models;

namespace StudyBuddy.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(Message message);
    }
}
