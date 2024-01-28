using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentById(Guid Id, bool trackChanges);
    }
}
