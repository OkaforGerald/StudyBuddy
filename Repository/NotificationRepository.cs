using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
            return FindByCondition(x => x.OwnerId.Equals(userId), trackChanges)
                .OrderByDescending(x => x.CreatedAt);
        }

        public async Task<Notification> GetNotificationsForMatch(string matcherId, string matchedId, bool trackChanges)
        {
            return await FindByCondition(x => x.OwnerId.Equals(matchedId) && x.MatcherId.Equals(matcherId) && x.MatchedId.Equals(matchedId), trackChanges)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetNumberOfProfileViews(string userId, bool trackChanges)
        {
            var profileViews = await FindByCondition(x => (x.OwnerId.Equals(userId) && x.NotifType == NotificationType.ProfileView) && (x.CreatedAt >= DateTime.Now.AddDays(-30)), trackChanges)
                .ToListAsync();

            return profileViews.Count();
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
