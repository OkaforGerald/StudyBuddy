using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CourseOfStudy
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Department? Department { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
