using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.Data_Transfer;

namespace Services.Contracts
{
    public interface IMessageService
    {
        Task<List<string>> GetMessagedPeople(string username);

        Task<HubMessage> CreateMessage(string currentUser, string otherUser, string message);

        Task<List<HubMessage>> GetThreadForUsers(string currentUser, string otherUser);
    }
}
