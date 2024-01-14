using SharedAPI.Data;

namespace StudyBuddy.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(HubMessage message);

        Task ReceiveThread(List<HubMessage> messages);
    }
}
