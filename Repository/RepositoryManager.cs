using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext context;
        private readonly Lazy<IMessageRepository> messageRepository;

        public RepositoryManager(RepositoryContext context)
        {
            this.context = context;
            this.messageRepository = new Lazy<IMessageRepository>(new MessageRepository(context));
        }

        public IMessageRepository MessageRepository => messageRepository.Value;

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
