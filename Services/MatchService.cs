using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public MatchService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task AcknowledgeMatch(string MatchAcknowledger, string MatchRequester, bool trackChnages)
        {
            var acknowledger = await userManager.FindByNameAsync(MatchAcknowledger);
            var requester = await userManager.FindByNameAsync(MatchRequester);
            var match = await repositoryManager.MatchRepository.GetMatchBetweenUsers(acknowledger.Id,requester.Id, trackChnages);
            var matchNotification = await repositoryManager.NotificationRepository.GetNotificationsForMatch(requester.Id, acknowledger.Id, trackChnages);
            repositoryManager.NotificationRepository.DeleteNotification(matchNotification);

            if(match is null)
            {
                throw new Exception("Match Can't be found");
            }
            
            requester.Matches++;
            acknowledger.Matches++;

            await userManager.UpdateAsync(acknowledger);
            await userManager.UpdateAsync(requester);

            Notification notif = new Notification
            {
                OwnerId = acknowledger.Id,
                MatcherId = requester.Id,
                MatchedId = acknowledger.Id,
                NotifType = NotificationType.AckMatch,
                CreatedAt = DateTime.UtcNow
            };
            Notification notif1 = new Notification
            {
                OwnerId = requester.Id,
                MatcherId = requester.Id,
                MatchedId = acknowledger.Id,
                NotifType = NotificationType.AckMatch,
                CreatedAt = DateTime.UtcNow
            };
            Message message = new Message
            {
                messageContent = "👋",
                RecipientUsername = requester.UserName,
                SnederUsername = acknowledger.UserName,
                RecipientId = requester.Id,
                SenderId = acknowledger.Id,
                CreatedAt = DateTime.UtcNow
            };

            repositoryManager.MessageRepository.CreateMessage(message);
            match.Status = MatchStatus.Friends;
            match.CreatedAt = DateTime.UtcNow;
            repositoryManager.NotificationRepository.CreateNotification(notif);
            repositoryManager.NotificationRepository.CreateNotification(notif1);
            await repositoryManager.Save();
        }

        public async Task DeclineMatch(string MatchAcknowledger, string MatchRequester, bool trackChanges)
        {
            var acknowledger = await userManager.FindByNameAsync(MatchAcknowledger);
            var requester = await userManager.FindByNameAsync(MatchRequester);
            var match = await repositoryManager.MatchRepository.GetMatchBetweenUsers(acknowledger.Id, requester.Id, trackChanges);
            var matchNotification = await repositoryManager.NotificationRepository.GetNotificationsForMatch(requester.Id, acknowledger.Id, trackChanges);
            repositoryManager.NotificationRepository.DeleteNotification(matchNotification);

            if (match is null)
            {
                throw new Exception("Match Can't be found");
            }
            Notification notif1 = new Notification
            {
                OwnerId = requester.Id,
                MatcherId = requester.Id,
                MatchedId = acknowledger.Id,
                NotifType = NotificationType.DecMatch,
                CreatedAt = DateTime.UtcNow
            };

            repositoryManager.NotificationRepository.CreateNotification(notif1);
            repositoryManager.MatchRepository.DeleteMatch(match);
            await repositoryManager.Save();
        }

            public async Task CreateMatch(string MatchRequester, string MatchResponseer)
        {
            var responseer = await userManager.FindByNameAsync(MatchResponseer);
            var requester = await userManager.FindByNameAsync(MatchRequester);

            if (responseer == null)
            {
                throw new Exception();
            }

            var match = new Entities.Models.Match
            {
                MatcherId = requester.Id,
                MatchedId = responseer.Id,
                Status = MatchStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            Notification notif = new Notification
            {
                OwnerId = responseer.Id,
                MatcherId = requester.Id,
                MatchedId = responseer.Id,
                NotifType = NotificationType.MatchRequest,
                CreatedAt = DateTime.UtcNow
            };

            repositoryManager.NotificationRepository.CreateNotification(notif);
            repositoryManager.MatchRepository.CreateMatch(match);
            await repositoryManager.Save();
        }
    }
}
