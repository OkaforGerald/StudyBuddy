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
    public class SelectionRepository : RepositoryBase<ProficiencySelection>, ISelectionRepository
    {
        public SelectionRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<ProficiencySelection>> GetProficiencySelectionsForUser(Guid userId, bool trackChanges)
        {
            return await FindByCondition(x => x.UserDetailsId == userId, trackChanges)
                .Include(x => x.Course)
                .OrderBy(x => x.Course)
                .ToListAsync();
        }

        public void CreateSelection(ProficiencySelection selection)
        {
            Create(selection);
        }
    }
}
