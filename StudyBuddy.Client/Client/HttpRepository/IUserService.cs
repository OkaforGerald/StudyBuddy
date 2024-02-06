using SharedAPI.Data;
using SharedAPI.RequestFeatures;
using StudyBuddy.Client.Client.Features;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetUserDetails(string username);

        Task<PagingResponse<UsersDto>> GetUsers(RequestParameters parameters);
    }
}
