using System.Text.Json;
using SharedAPI.Data;

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
    }
}
