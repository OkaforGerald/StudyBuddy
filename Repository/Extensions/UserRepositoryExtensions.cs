using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository.Extensions
{
    public static class UserRepositoryExtensions
    {
        public static IQueryable<User> GetUsers(this IQueryable<User> users, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return users;
            }
            var SearchTermTrimmed = searchTerm.Trim().ToLower();

            return users.Where(p => p.UserName.ToLower().Contains(SearchTermTrimmed) ||
            p.FirstName.ToLower().Contains(SearchTermTrimmed) ||
            p.LastName.ToLower().Contains(SearchTermTrimmed));
        }
    }
}
