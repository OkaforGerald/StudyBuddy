using Blazored.LocalStorage;
using Microsoft.AspNetCore.WebUtilities;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;
using StudyBuddy.Client.Client.Features;
using System.Text.Json;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public class NotificationService : INotificationService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public NotificationService(ILocalStorageService localStorageService, HttpClient client)
        {
            _localStorageService = localStorageService;
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<PagingResponse<NotificationDto>> GetNotifications(RequestParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>()
            {
                ["PageNumber"] = parameters.PageNumber.ToString()
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("notifications", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<NotificationDto>
            {
                Items = JsonSerializer.Deserialize<List<NotificationDto>>(content, _options),
                Metadata = JsonSerializer.Deserialize<Metadata>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }
    }
}
