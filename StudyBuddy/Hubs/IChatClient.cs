namespace StudyBuddy.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
