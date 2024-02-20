using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface INotificationRepository
    {
        Task<IQueryable<Notification>> GetNotificationsForUser(string userId, bool trackChanges);

        void DeleteNotification(Notification notification);

        void CreateNotification(Notification notification);
    }
}
