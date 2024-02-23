using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public NotificationService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<(List<NotificationDto> notifications, Metadata metadata)> GetNotificationsForUser(string username, RequestParameters parameters, bool trackChanges)
        {
            var user = await userManager.FindByNameAsync(username);
            var notifs = await repositoryManager.NotificationRepository.GetNotificationsForUser(user.Id, trackChanges);
            var notifications = PagedList<Notification>.ToPagedList(notifs, parameters.PageNumber, parameters.PageSize);

            var notificationResponse = new List<NotificationDto>();

            foreach(var notification in notifications)
            {
                if(notification.NotifType == NotificationType.ProfileView)
                {
                    var Viewer = await userManager.FindByIdAsync(notification.ProfileViewerId);

                    notificationResponse.Add(new NotificationDto
                    {
                        Owner = user.UserName,
                        ProfileViewer = Viewer.UserName,
                        NotifType = notification.NotifType.ToString(),
                        CreatedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var matcher = await userManager.FindByIdAsync(notification.MatcherId);
                    var matched = await userManager.FindByIdAsync(notification.MatchedId);

                    notificationResponse.Add(new NotificationDto
                    {
                        Owner = user.UserName,
                        Matcher = matcher.UserName,
                        Matched = matched.UserName,
                        NotifType = notification.NotifType.ToString(),
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            return (notifications: notificationResponse, metadata: notifications.metadata);
        }
    }
}
