using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum Modes
    {
        Virtual = 0,
        OnSite = 1
    }

    public class UserDetails
    {
        public Guid Id { get; set; }

        public Modes Mode { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? Website { get; set; }

        public string? Github { get; set; }

        public string? Twitter { get; set; }

        public decimal Rating { get; set; } = decimal.Zero;
        public int RatingNum { get; set; }

        public CourseOfStudy? Course { get; set; }
        public Guid CourseId {  get; set; }

        public Department? Department { get; set; }
        public Guid DepartmentId { get; set; }

        public User? User { get; set; }
        public string? UserId { get; set; }

        public ICollection<ProficiencySelection>? ProficientCourses { get; set; }
    }
}
