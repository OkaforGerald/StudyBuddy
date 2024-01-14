using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Department? Department { get; set; }
        public Guid DepartmentId { get; set; }

        public ICollection<ProficiencySelection>? ProficiencySelections { get; set; }
    }
}
