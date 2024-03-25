using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;
using StudyBuddy.Client.Client.Features;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public UserService(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<UserDetailsDto> GetUserDetails(string username)
        {
            var response = await _client.GetAsync($"users/{username}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var details = JsonSerializer.Deserialize<UserDetailsDto>(content, _options);
            return details;
        }

        public async Task<PagingResponse<UsersDto>> GetUsers(RequestParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>()
            {
                ["PageNumber"] = parameters.PageNumber.ToString(),
                ["SearchTerm"] = parameters.SearchTerm == null ? "" : parameters.SearchTerm,
                ["CourseSearchTerm"] = parameters.CourseSearchTerm == null ? "" : parameters.CourseSearchTerm,
                ["DepartmmentId"] = parameters.DepartmmentId.ToString(),
                ["CourseId"] = parameters.CourseId.ToString()
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("users", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<UsersDto>
            {
                Items = JsonSerializer.Deserialize<List<UsersDto>>(content, _options),
                Metadata = JsonSerializer.Deserialize<Metadata>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<DashboardDto> Dashboard()
        {
            var response = await _client.GetAsync("users/dashboard");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var details = JsonSerializer.Deserialize<DashboardDto>(content, _options);
            return details;
        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var response = await _client.GetAsync(@"school/departments");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var details = JsonSerializer.Deserialize<List<DepartmentDto>>(content, _options);
            return details;
        }

        public async Task<List<CourseDto>> GetCourses()
        {
            var response = await _client.GetAsync(@"school/courses");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var course = JsonSerializer.Deserialize<List<CourseDto>>(content, _options);
            return course;
        }

        public async Task<List<CourseDto>> GetCourses(Guid Id)
        {
            var response = await _client.GetAsync($"school/departments/{Id}/courses");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var details = JsonSerializer.Deserialize<List<CourseDto>>(content, _options);
            return details;
        }

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = postContent;
                return imgUrl;
            }
        }

        public async Task AddDetails(AddDetailsDto addDetails)
        {
            var content = JsonSerializer.Serialize(addDetails);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var detailsResult = await _client.PostAsync("users/details", bodyContent);
            var detailsContent = await detailsResult.Content.ReadAsStringAsync();

            if (!detailsResult.IsSuccessStatusCode)
            {
                
            }

            //return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }
    }
}
