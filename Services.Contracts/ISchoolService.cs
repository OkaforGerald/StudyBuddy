using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedAPI.Data;

namespace Services.Contracts
{
    public interface ISchoolService
    {
        Task<List<DepartmentDto>> GetDepartments(bool trackChanges);

        Task<List<CourseDto>> GetCourseByDeptId(Guid Id, bool trackChanges);

        Task<List<CourseDto>> GetCourses(bool trackChanges);
    }
}
