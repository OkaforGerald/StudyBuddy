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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<Department> GetDepartmentById(Guid Id, bool trackChanges) => await FindByCondition(x => x.Id == Id, trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Department>> GetDepartments(bool trackChanges) => await FindAll(trackChanges).ToListAsync();
    }
}
