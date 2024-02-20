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
    public class MatchRepository : RepositoryBase<Match>, IMatchRepository
    {
        public MatchRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public async Task<Match> GetMatchById(Guid Id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == Id, trackChanges)
                .FirstOrDefaultAsync();
        }

        public async Task<Match> GetMatchBetweenUsers(string acknowledgerId, string requesterId, bool trackChanges)
        {
            var match = await FindByCondition(x => x.MatcherId.Equals(requesterId) && x.MatchedId.Equals(acknowledgerId) || x.MatcherId.Equals(acknowledgerId) && x.MatchedId.Equals(requesterId), trackChanges)
                .FirstOrDefaultAsync();

            return match;
        }

        public async Task<int> GetMatchStatusBetweenUsers(string requesterId, string userId, bool trackChanges)
        {
            var match = await FindByCondition(x => ((x.MatcherId.Equals(requesterId) && x.MatchedId.Equals(userId)) || (x.MatcherId.Equals(userId) && x.MatchedId.Equals(requesterId))), trackChanges)
                .FirstOrDefaultAsync();
            
            return match is null ? 3 : (int)(object)match.Status;
        }

        public async Task<IEnumerable<Match>> GetMatchesForUser(string userId, bool trackChanges)
        {
            return await FindByCondition(x => x.MatcherId.Equals(userId) || x.MatchedId.Equals(userId), trackChanges)
                .ToListAsync();
        }

        public void CreateMatch(Match match)
        {
            Create(match);
        }
    }
}
