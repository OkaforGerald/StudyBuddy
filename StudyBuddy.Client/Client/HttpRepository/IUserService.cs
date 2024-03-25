using SharedAPI.Data;
using SharedAPI.RequestFeatures;
using StudyBuddy.Client.Client.Features;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetUserDetails(string username);

        Task<PagingResponse<UsersDto>> GetUsers(RequestParameters parameters);

        Task<List<DepartmentDto>> GetDepartments();

        Task<List<CourseDto>> GetCourses();

        Task<List<CourseDto>> GetCourses(Guid Id);

        Task<DashboardDto> Dashboard();

        Task AddDetails(AddDetailsDto addDetails);

        Task<string> UploadProductImage(MultipartFormDataContent content);
    }
}
