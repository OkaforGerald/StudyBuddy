using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using SharedAPI.RequestFeatures;

namespace Repository
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IQueryable<Notification>> GetNotificationsForUser(string userId, bool trackChanges)
        {
            return FindByCondition(x => x.Owner.Equals(userId), trackChanges);
        }

        public void DeleteNotification(Notification notification)
        {
            Delete(notification);
        }

        public void CreateNotification(Notification notification)
        {
            Create(notification);
        }
    }
}
