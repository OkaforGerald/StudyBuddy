using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum Proficiency
    {
        Basic = 1,
        Competent = 2,
        Advanced = 3,
        Expert = 4,
        Master = 5
    }
    public class ProficiencySelection
    {
        public Guid Id { get; set; }

        public Proficiency Level { get; set; }

        public Guid UserDetailsId { get; set; }
        public UserDetails? UserDetails { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
