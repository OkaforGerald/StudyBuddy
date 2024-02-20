using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IMatchRepository
    {
        Task<Match> GetMatchById(Guid Id, bool trackChanges);

        Task<IEnumerable<Match>> GetMatchesForUser(string userId, bool trackChanges);

        Task<Match> GetMatchBetweenUsers(string acknowledgerId, string requesterId, bool trackChanges);

        Task<int> GetMatchStatusBetweenUsers(string requesterId, string userId, bool trackChanges);

        void CreateMatch(Match match);
    }
}
