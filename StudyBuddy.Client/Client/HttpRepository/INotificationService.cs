using SharedAPI.Data;
using SharedAPI.RequestFeatures;
using StudyBuddy.Client.Client.Features;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface INotificationService
    {
        Task<PagingResponse<NotificationDto>> GetNotifications(RequestParameters parameters);
    }
}
