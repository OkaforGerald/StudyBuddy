using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Services.Contracts;
using Shared.Data_Transfer;

namespace Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepositoryManager manager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public MessageService(IRepositoryManager manager, UserManager<User> userManager, IMapper mapper)
        {
            this.manager = manager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<string>> GetMessagedPeople(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if(user == null)
            {
                throw new Exception(); // Come back to this
            }

            var peopleSet = manager.MessageRepository.GetContacts(user.Id);

            var peopleMessaged = peopleSet.Select(x => userManager.FindByIdAsync(x).Result.UserName)
                .ToList();

            return peopleMessaged;
        }

        public async Task<HubMessage> CreateMessage(string currentUser, string otherUser, string message)
        {
            var recipient = await userManager.FindByNameAsync(otherUser);
            var sender = await userManager.FindByNameAsync(currentUser);

            Message newMessage = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                RecipientUsername = otherUser,
                SnederUsername = currentUser,
                CreatedAt = DateTime.UtcNow,
                messageContent = message
            };

            manager.MessageRepository.CreateMessage(newMessage);
            await manager.Save();

            return mapper.Map<HubMessage>(newMessage);
        }

        public async Task<List<HubMessage>> GetThreadForUsers(string currentUser, string otherUser)
        {
            var messages = await manager.MessageRepository.GetThreadForUsers(currentUser, otherUser);

            return mapper.Map<List<HubMessage>>(messages);
        }
    }
}
