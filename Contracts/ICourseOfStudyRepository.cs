using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICourseOfStudyRepository
    {
        Task<CourseOfStudy> GetCourseById(Guid Id, bool trackChanges);

        Task<IEnumerable<CourseOfStudy>> CourseByDepartmentId(Guid Id, bool trackChanges);
    }
}
