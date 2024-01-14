using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Repository
{
    public class UserRepository : RepositoryBase<UserDetails>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context) { }

        public List<UserDetails> GetUserDetails(string id, bool trackChanges)
        {
            return FindByCondition(x => x.UserId == id, trackChanges)
                .ToList();
        }
    }
}
