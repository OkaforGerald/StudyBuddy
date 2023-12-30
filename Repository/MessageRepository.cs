using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Repository
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
        public HashSet<string> GetContacts(string userId)
        {
            return FindByCondition(x => x.SenderId == userId, trackChanges: false)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.RecipientId)
                .ToHashSet<string>();
        }
    }
}
