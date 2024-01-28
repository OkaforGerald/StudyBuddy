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
    public class UserRepository : RepositoryBase<UserDetails>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context) { }

        public async Task<UserDetails> GetUserDetails(string id, bool trackChanges)
        {
            return await FindByCondition(x => x.UserId == id, trackChanges)
                .Include(x => x.Course)
                .FirstOrDefaultAsync();
        }
    }
}
