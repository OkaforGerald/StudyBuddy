using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IMatchService
    {
        Task CreateMatch(string MatchRequester, string MatchResponseer);

        Task AcknowledgeMatch(string MatchAcknowledger, string MatchRequester, bool trackChanges);

        Task DeclineMatch(string MatchAcknowledger, string MatchRequester, bool trackChanges);
    }
}
