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

            if(match is null)
            {
                throw new Exception("Match Can't be found");
            }
            
            requester.Matches++;
            acknowledger.Matches++;

            await userManager.UpdateAsync(acknowledger);
            await userManager.UpdateAsync(requester);

            match.Status = MatchStatus.Friends;
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
                Status = MatchStatus.Pending
            };

            Notification notif = new Notification
            {
                MatcherId = requester.Id,
                MatchedId = responseer.Id,
                NotifType = NotificationType.MatchRequest
            };

            repositoryManager.NotificationRepository.CreateNotification(notif);
            repositoryManager.MatchRepository.CreateMatch(match);
            await repositoryManager.Save();
        }
    }
}
