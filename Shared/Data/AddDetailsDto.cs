using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SharedAPI.Data
{
    public class AddDetailsDto
    {
        public string? LinkedinUrl { get; set; }

        public string? Website { get; set; }

        public string? Github { get; set; }

        public string? Twitter { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        public Guid CourseId { get; set; }

        public Guid DepartmentId { get; set; }

        public int Mode { get; set; }

        public string? ImageUrl { get; set; }

        public List<Guid> ProficientCourses { get; set; } = new List<Guid> { Guid.Empty, Guid.Empty, Guid.Empty};
        public List<int> ProficiencyInts { get; set; } = new List<int> { 0, 0, 0 };
    }
}
