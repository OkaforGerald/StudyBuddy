using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
        public HashSet<string> GetContacts(string userId)
        {
            var messages = FindByCondition(x => x.SenderId == userId || x.RecipientId == userId, trackChanges: false)
                .OrderByDescending(x => x.CreatedAt);

            var senders = messages.Select(x => x.SenderId);
            var recipient = messages.Select(x => x.RecipientId);

            var allContacts = senders.Union(recipient).ToHashSet<string>();
            allContacts.Remove(userId);

            return allContacts;
        }

        public async Task<List<Message>> GetThreadForUsers(string currentUser, string otherUser)
        {
            return await FindByCondition(x => (x.SnederUsername.Equals(currentUser) && x.RecipientUsername.Equals(otherUser)) || (x.SnederUsername.Equals(otherUser) && x.RecipientUsername.Equals(currentUser)), trackChanges: false)
                .OrderByDescending(x => x.CreatedAt)
                .Take(10)
                .Reverse()
                .ToListAsync();
        }

        public void CreateMessage(Message message)
        {
            Create(message);
        }
    }
}
