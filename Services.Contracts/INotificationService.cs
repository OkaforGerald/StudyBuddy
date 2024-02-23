using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;

namespace Services.Contracts
{
    public interface INotificationService
    {
        Task<(List<NotificationDto> notifications, Metadata metadata)> GetNotificationsForUser(string username, RequestParameters parameters, bool trackChanges);
    }
}
