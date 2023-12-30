using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Services.Contracts;

namespace Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepositoryManager manager;
        private readonly UserManager<User> userManager;

        public MessageService(IRepositoryManager manager, UserManager<User> userManager)
        {
            this.manager = manager;
            this.userManager = userManager;
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
    }
}
