using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IMessageRepository
    {
        HashSet<string> GetContacts(string userId);

        Task<List<Message>> GetThreadForUsers(string currentUser, string otherUser);

        void CreateMessage(Message message);
    }
}
