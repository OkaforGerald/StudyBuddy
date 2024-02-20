namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface IMatchService
    {
        Task CreateMatch(string username);

        Task AckMatch(string username);
    }
}
