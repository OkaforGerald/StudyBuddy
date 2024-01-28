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
    public class CourseofStudyRepository : RepositoryBase<CourseOfStudy>, ICourseOfStudyRepository
    {
        public CourseofStudyRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<CourseOfStudy> GetCourseById(Guid Id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == Id, trackChanges)
                .FirstOrDefaultAsync();
        }
    }
}
