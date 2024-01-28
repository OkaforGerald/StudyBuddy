using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SharedAPI.Data
{
    public class UserDetailsDto
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Username { get; set; }

        public string? CourseOfStudy { get; set; }
        public string? Department { get; set; }

        public string? ImageUrl { get; set; } = @"https://localhost:7011/Images/defaultavi.jpg";

        public string? Bio {  get; set; }

        public string? Mode { get; set; }

        public string? LinkedinUrl { get; set; }

        public string? Website { get; set; }

        public string? Github { get; set; }

        public string? Twitter { get; set; }

        public decimal Rating { get; set; } = decimal.Zero;
        public int RatedBy { get; set; }

        public bool RequestedByOwner { get; set; } = false;

        public ICollection<ProficientCoursesDto>? ProficientCourses { get; set; }
    }
}
