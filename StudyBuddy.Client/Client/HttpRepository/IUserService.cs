using SharedAPI.Data;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetUserDetails(string username);
    }
}
