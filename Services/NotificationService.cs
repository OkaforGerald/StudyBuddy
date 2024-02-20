using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using SharedAPI.RequestFeatures;

namespace Services
{
    public class NotificationService
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

        public async Task<(PagedList<Notification> notifications, Metadata metadata)> GetNotificationsForUser(string username, RequestParameters parameters, bool trackChanges)
        {
            var user = await userManager.FindByNameAsync(username);
            var notifs = await repositoryManager.NotificationRepository.GetNotificationsForUser(user.Id, trackChanges);
            var notifications = PagedList<Notification>.ToPagedList(notifs, parameters.PageNumber, parameters.PageSize);

            return (notifications: notifications, metadata: notifications.metadata);
        }
    }
}
