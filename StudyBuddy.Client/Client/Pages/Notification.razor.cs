using Microsoft.AspNetCore.Components;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;
using StudyBuddy.Client.Client.Features;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Notification
    {
        public RequestParameters parameters { get; set; } = new();

        private PagingResponse<NotificationDto> _notifications = new();

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            _notifications = await NotificationService.GetNotifications(parameters);
        }

        private async Task PageChanged(int PageNumber)
        {
            parameters.PageNumber = PageNumber;
            _notifications = await NotificationService.GetNotifications(parameters);
        }

        private async Task AckMatch(string username)
        {
            await MatchService.AckMatch(username);
            await OnInitializedAsync();
        }

        private async Task DecMatch(string username)
        {
            await MatchService.DecMatch(username);
            await OnInitializedAsync();
        }

        public void Dispose()
        {
            Interceptor.DisposeEvent();
        }
    }
}
